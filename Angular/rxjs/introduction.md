RxJS is a library for composing asynchronous and event-based programs
by using `observable sequences`.
it provided one core type, the `Observable`, satellite types (Observer, Schemulers, Subjects) and Operators inspired by `Array` methods (`map`, `filter`, `reduce`, `every`, etc) to allow handing asynchronous events as collections.

| Think of RxJS as Lodash for events.

ReactiveX combines the
`Observer pattern` with the
`Iterator pattern` and
`functional programming with collections` to filter the need for an ideal way of managing sequences of events.

the essential consepts in RxJS which solve async event management are:
- `Observable`: represents the idea of an invokable collection of future values or events.
- `Observer`: is a collection of callbacks that knows how to listen to values delivered by the Observable.
- `Subscription`: represents the exxecution of an Observable, is primarily useful for cancelling the execution.
- `Operators`: are pure functions that enable a functional programming style of dealing with collections with operations like `map`, `filter`, `concat`, `reduce`, etc.
- `Subject`: is equivalent to an EventEmitter and the only way of multicasting a value or event to multiple Observers.
- `Schedulers`: are centralized dispatchers to control concurrency, allowing us to coordinate when computation happens on e.g. `setTimeout` or `requestAnimationFrame` or others.

-- examples
```ts
// normally you register event listeners.
    document.addEventListener('click', () => console.log('clicked'));
// using RxJS you create an observable instead.
    import { fromEvent } from 'rxjs';

    fromEvent(document, 'click').subscribe(() => console.log('clicked'));
```

##### purity
what makes RxJS powerful is its ability to produce values using pure functions. that means your code is less prone to errors.

normally you would create an impure function, where other pieces of your code can mess up your state.
```ts
let count = 0;
document.addEventListener('click', () => console.log(`clicked ${++count} times`));

// using RxJS you isolate the state.
import { fromEvent, scan } from 'rxjs';

fromEvent(document, 'click')
    .pipe(scan((count) => count +1, 0))
    .subscribe((count) => console.log(`click ${count} times`));
```
the -scan- operator works just like -reduce` for arrays. it takes a value which is exposed to a callback. the returned value of the callback will then become the next value exposed the next time the callback runs.


##### Values
you can transform the values passed through your observables.
hear's how you can add the current mouse x position for every click in plain javascript
```ts
let count = 0;
const rate = 1000;
let lastClick = Date.now() - rate;

document.addEventListener('click', (event) => {
    if(Date.now() - lastclick >= rate){
        count += event.clickX;
        console.log(count);
        lastClick = Date.now();
    }
})
// with RxJS
import { fromEvent, throttleTime, map, scan } from 'rxjs';

fromEvent(document, 'click')
    .pipe(
        throttleTime(1000),
        map((event) => event.clientX),
        scan((count, clientX) => count + clickX, 0)
    )
    .subscribe((count) => console.log(count));
```
other value producing operators are [`pluck`](https://rxjs.dev/api/operators/pluck), [`pairwise`](https://rxjs.dev/api/operators/pairwise), [`samble`](https://rxjs.dev/api/operators/sample), etc. 