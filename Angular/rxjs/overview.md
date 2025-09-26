## Observable
observables are lazy Push collections of multiple values. they fill the missing spot in the following table:

|       | SINGLE    |   MULTIPLE    |
|-------|-----------|---------------|
| Pull  | Function  |   Iterator    |
| Push  | Promise   | Observable    |

example: the following is an Observable the pushes the value 1, 2, 3 immediately (synchronously) when subscribed, and the value 4 after one second has passed since the subscribe call, then completes:
```ts
import { Observable } from 'rxjs';

const observable = new Observable((subscriber) => {
    subscriber.next(1);
    subscriber.next(2);
    subscriber.next(3);
    setTimeout(() => {
        subscriber.next(4);
        subscriber.complete();
    }, 1000);
})
```
to invoke the Observable and see these values, we need to `subscribe` to it:
```ts
import { Observable } from 'rxjs';

const observable = new Observable((subscriber) => {
    subscriber.next(1);
    subscriber.next(2);
    subscriber.next(3);
    setTimeout(() => {
        subscriber.next(4);
        subscriber.complete();
    }, 1
});

console.log('just before subscribe');
observable.subscribe({
    next(x){ 
        console.log('got value ', x);
    },
    error(err){
        console.error('someting wrong occurred', err);
    },
    complete(){
        console.log('done');
    }
});
console.log('just after subscribe');
```
which executes as such on the console:
```ts
just before subscribe
got value 1
got value 2
got value 3
just after subscribe
got value 4
done
```

##### Pull versus Push
`Pull` and `Push` are two different protocols that describe how a data **Producer** can communicate with a data **Cunsumer**.
**Pull systems**, the consumer determines when it receives data from the sata Producer, The Producer itself is unaware of then the data will be delivered to the Consumer.

every JavaScript Function is a Pull system. the function is a Producer of data, and the code tat calls the function is consuming it by "pulling" out a single return value from its call.

ES2015 introduced __generator functions and iterators__ (`function*`), another type of Pull system. Code that calls `iterator.next()` is the Consumer, "pulling" out multiple values from the iterator (the Producer).
|       |   PRODUCER    |   CONSUMER    |
|-------|---------------|---------------|
|Pull   | **Passive**: produces data when requested. | **Active**: decides when data is requested.  |
|Push   | **Active**: produces data at its own pace. | **Passive**: reacts to received data.        |
**Push** systems, the Producer determines when to send data to the Consumer. the consumer is unaware of when it will receive that data.

promises are the most common type of Push system in javascript today. a promise (the producer) delivers a resolved value to registered callbacks (the consumers), but unlike functions, it is the Promise which is in charge of determining precisely when that value is "pushed" to the callbacks.

RxJS introduces Observables, a new Push system for JavaScript. An Observable is a Producer of multiple values, "pushing" them to Observers (Consumers).
- A **Function** is a lazily evaluated computation that synchronously returns a single value on invocation.
- A **Generator** is a lazily evaluated computation that synchronously returns zero to (potentially) infinite values on iteration.

- A **Promise** is a computation that may (or may not) eventually return a single value.
- An **Observable** is a lazily evaluated computation that can synchronously or asynchronously return zero to (potentially) infinite values from the time it's invoked owards.

| for more info about what to use when converting Observables to Promises, please refer [`to-promise`](https://rxjs.dev/deprecations/to-promise) 


### Observables as generalizations of functions
countrary to popular claims, Observables are not like EventEmitters nor are they like Promises from multiple values.
Observables may act like EventEmitters in some cases, manely when they are multicasted using RxJS Subjects, but usually they don't act like EventEmitters.

| Observables are like functions with zero arguments, but generalize those to allow multiple values.
```ts
function foo(){
    console.log("Hello");
    return 39;
}

const x = foo.call(); // same as foo()
console.log(x);
const y = foo.call(); // same as foo()
console.log(y);
```
we expect to see as output:
```ts
import { Observable } from 'rxjs';

const foo = new Observable((subscriber) => {
    console.log('Hello');
    subscriber.next(39);
});

foo.subscribe((x) => { console.log(x); });
foo.subscribe((y) => { console.log(y); });
```
and the output is the same:
```
"Hello"
39
"Hello"
39
```

| Subscribing to an Observable is analogous to calling a Function.
some people claim that Observables are asynchronous. that is not true. if you surround a function call with logs, like this:
```ts
console.log('before');
console.log(fun.call());
console.log('after');
// you will see the output:
"before"
"Hello"
39
"after"

// and this is the same behavior with Observables:
console.log('before');
foo.subscribe(x) => { console.log(x) });
console.log('after');
// and the output is:
"before"
"Hello"
39
"after"
```
which proves the subscription of `foo` was entriely synchronous, ust like a function.

| Observables are able to deliver values either synchromously or asynchronously.

what is the difference between an Observable and a function? 
**Observables can "return" multiple values over time**, something which functions cannot. you can't do this:
```ts
function foo(){
    console.log("Hello");
    return 39;
    return 100; //dead code. will never happen
}
/// functions can only return one value. Observables, however, can do this:
import { Observable } from 'rxjs';

const foo = new Observable((subscriber) => {
    console.log('Hello');
    subscriber.next(39);
    subscriber.next(100); // "return" another value
    subscriber.next(200); // "return" yet another
});

console.log('before');
foo.subscribe((x) => { console.log(x); });
console.log('after');

// with synchronous output
"before"
"Hello"
39
100
200
"after"
```

but you can also "return" values asynchronously:
```ts
import { Observable } from 'rxjs';

const foo = new Observable((subscriber) => {
    console.log('Hello');
    subscriber.next(39);
    subscriber.next(100);
    subscriber.next(200);
    setTimeout(() => {subscriber.next(300);}, 1000); // happens asynchronously
});

console.log('before');
foo.subscribe((x) => { console.log(x); });
console.log('after');
// with output:
"before"
"Hello"
39
100
200
"after"
300
```
conslusion:
- `func.call()` means "give me one value synchronously"
- `observable.subscribe()` means "give me any amount of values, either synchronously or asynchronously"

### Anatomy of an Observable
Observables are **crated** using `new Observable` or a creation operator, are **subscribed** to with an Observer, **execute** to deliver `next` / `error` / `complete` notifications to the Observer and their execution may be `disposed`. these four aspects are all encoded in an Observable instance but some of these aspects are related to other types, like Observer and Subscription.

Core Observable concerns:
- **Creating** Observables
- **Subscribing** to Observables
- **Executing** to Observable
- **Disposing** Observables

#### Creating Observables
the `Observable` constructor takes one argument: the `subscribe` function.

the following example creates an Observable to emit the string 'hi' every second to a subscriber.
```ts
import { Observable } from 'rxjs';

const obsesrvable = new Observable(function subscribe(subscriber) {
    const id = setInterval(() => {
        subscriber.next('hi');
    }, 1000);
});
```
| Observables can be created with new Observable. Let's look at what subscribing means.

#### Subscribing to Observables
the Observable `observable` in the example can be subscribed to, like this:
```ts
observable.subscribe(x) => console.log(x));
```
it is not a coincidence that `observable.subscribe` and `subscribe` in `new Observable(function subscribe(subscriber){...})` have the same name. in the library, they are different, but for practical purposes you can consider them conceptually equal.

this shows how `subscribe` calls are not shared among multiple Observers of the same Observable. when call `observable.subscribe` with an Observer, the function `subscribe` in ` new Observable(function subscribe(subscriber){...})` is run for that given subscriber. each call to `observable.subscribe` targgers its own independent setup for that give subscriber.

| subscribing to an Observable is like calling a function, providing callbacks where the data will be delivered to.

this is drasticall different to event handler APIs like `addEventListener`/`removeEventListener`. with `observable.subscribe`, the give Observer is not registered as a listener in the Observable. the Observable does not even maintain a list of attached observers.

a `subscribe` call is simply a way to start an "Observable execution" and deliver values or events to an Observer of that execution.

#### Executing Observables
the code inside `new Observable(function subscribe(subscriber) {...})` represents an "Observable execution", a lazy computation that only happens for each Observer that subscribes. the execution produces multiple values over time, either synchonously or asynchronously.

there are three types of values an Observable Execution can deliver:
- "Next" notification: sends a value such as a Number, a String, an Object,etc.
- "Error" noticication: sends a JavaScript Error or exception.
- "Complete" notification: does not send a value.

"Next" notifications are the most important and most common type: they represent actual data being delivered to a subscriber.
"Error" and "Complete" notifications may happen only once during the Observable Execution, and there can only be either one of them.

these constaints are expressed best in the so-called Observable Grammar or Contract, written as a regular expression:
```ts
next*(error|complete)?
```
| in an Observable Execution, zero to infinite Next notifications may be delivered. if either an Error or Complete notification is delivered , then nothing else can be delivered afterwards.

the following is an example of an Observable execution the delivers three Next notifications, then completes:
```ts
import { Observable } from 'rxjs';

const observable = new Observable(function subscribe(subscriber){
    subscriber.next(1);
    subscriber.next(2);
    subscriber.next(3);
    subscriber.complete();
});
// observables strictly adhere to the Observable Contract, so the following code whould not deliver the Next notification 4:
import { Observable } from 'rxjs';

const observable = new Observable(function subscribe(subscriber){
    subscriber.next(1);
    subscriber.next(2);
    subscriber.next(3);
    subscriber.complete();
    subscriber.next(4); // is not delivered because it would violate the contract
});
// it is a good idea to wrap any code in `subscribe` with `try/catch` block that will deliver an Error notification if it catches an exception:
import { Observable } from 'rxjs';

const observable = new Observable(function subscribe(subscriber){
    try{
        subscriber.next(1);
        subscriber.next(2);
        subscriber.next(3);
        subscriber.complete();
    } catch (err) {
        subscriber.error(err); // delivers an error if it caught one
    }
})
```

#### Disposing Observable Executions
because Observable Executions may be infinite, and it's common for an Observer to want to abort execution in finite time, we need an API for canceling an execution. since each execution is exclusive to one Observer only, once the Observer is done receiving values, it has to have a way to stop the execution, in order to avoid wasting computation power or memory resources.

when `observable.subscribe` is called, the Observer gets attached to the newly created Obserbable execution. this call also returns an object, the `Subscription`:
```ts
const subscription = observable.subscribe((x) => console.log(x));
```
the subscription represents the ongoing execution, and has a minimal API which allows you to cancel that execution. read more about the [`Subscription type`](https://rxjs.dev/guide/subscription) with `subscription.unsubscribe()` you can cancel the ongoing execution:
```ts
import { from } from 'rxjs';

const observable = from([10, 20, 30]);
const subscription = observable.subscribe((x) => console.log(x));
// later:
subscription.unsubscribe();
```
| when you subscribe, you get back a Subscription, which represents the ongoing execution. just call unsubscribe() to cancel the execution.

each Observable must define how to dispose resources of that execution when we create the Observable using `create()`
you can do that by returning a custome `unsubscribe` function from within `function subscribe()`.

for instance, this is now we clear an interval execution set with `setInterval`:
```ts
import { Observable } from 'rxjs';

const observable = new Observable(function subscribe(subscriber){
    // keep track of the interval resource
    const intervalId = setInterval(() => { subscriber.next('hi')}, 1000);

    // provide a way of canceling and disposing the interval resource
    return function unsubscribe(){
        clearInterval(intervalId);
    }
});
```
just like `observable.subscribe` resembles `new Observable(function subscribe(){...})`, the `unsubscribe` we return from `subscribe` is conceptually equal to `subscription.unsubscribe`. in fact, if we remove the ReactiveX types surrounding these concepts, we're left with rather straightforward javascript.
```ts
funtion subscribe(subscriber){
    const intervalId = setInterval(() => { subscriber.next('hi');}, 1000);
    
    return function unsubscribe(){
        clearInterval(intervalId);
    };
}

const unsubscribe = subscribe({ next: (x) => console.log(x) });

// later:
unsubscriber();
```
the reason why we use RX types like Observable, Observer, and Subscription is to get safety (such as the Observable Contract) and composability with Operators.




## Observer
Observer is a consumer of values delivered by an Observable. Observers are simply a set of callbacks, one for each type of notification delivered by the Observable: `next`, `error` and `complete`. the following is an example of a typical Observer object:
```ts
const observer = {
    next: x => console.log('observer got a next value:', x),
    error: err => console.error('observer got an error:', err),
    complete: () => console.log('observer got a complete notification')
};

// to use the Observer, provide it to the `subscribe` of an Observable:
observable.subscribe(observer);
```

| Observers are juset objects with three callbacks, one for each type of notification that an Observable may deliver.

the example below is an `Observer` without the `complete` callback:
```ts
const observer = {
  next: x => console.log('Observer got a next value: ' + x),
  error: err => console.error('Observer got an error: ' + err),
};

when subsribing to and `Observable`, you may also just provide the next callback as an argument, without being attached to an `Observer` object, for instance like ithis:

observable.subscribe( x => console.log('observer got a next value', x));
```
internally in `observable.subscriber`, it will create an `Observer` object using the callback argument as the `next` handler.




