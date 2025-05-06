# [Angular Signal](https://angular.dev/guide/signals)
Angular Signals is a system that granularly tracks how and where your state is used throughout an application allowing the framwork to optimize rendering updates.

### What are signals?
a **signal** is a wrapper around a value that notifies interested consumers when that value changes. Signals can contain any value from primitives to complex data structures.

you read a signal's value by calling its getter function, which allows Angular to track where the signal is used.

Signals may be either *writable* or *read-only*.

#### Writable signals
writable signals provide an API for updating their values directly. You create writable signals by calling the `signal` function with the signal's initial value:
```ts
const count = signal(0);

// signals are getter functions - calling them reads their value.
console.log('The count is: ' + count());
```
to change the value of a writable signal, either `.set()` it directly:
```ts
count.set(3);
```
or use the `.update()` operation to compute a new value from the previous one:
```ts
// increment the count by 1.
count.update(value => value + 1);
```
writable signals have the type `WritableSignal`.


### Computed signals
**Compute signal** are read-only signals that derive their value from other signals. You define computed signals using the `computed` function and specifying a derivation:
```ts
const count: WritableSignal<number> = signal(0);
const doubleCount: Signal<number> = computed(() => count() * 2);
```
the `doubleCount` signal depends on the `count` signal. Whenever `count` updates, Angular knows that `doubleCount` needs to update as wall.

### Computed signals are both lazily evaluated and memoized
`doubleCount`'s derivation function does not run to calculate its value until the first time you read `doubleCount`. The calculated value is then cached and if you read `doubleCount` again, it will return the cached value without recalculating.

if you then change `count`, Angular knows that `douubleCount`'s cached value is no longer valid, and the next time you read `doubleCount` its new value will be calculated.

As a result, you can safely perform computationally expensive derivations in computed signals such as filtering arrays.

### Computed signals are not writable signals
you cannot directly assign values to a computed signal. that is,
```ts
doubleCount.set(3);
```
produces a compilation error because `doubleCount` is not a `WritableSignal`.

# Computed signal dependencies are dynamic
only the signals actually read during the derivation are tracked. from example in this `computed` the `count` signal is only read if the `showCount` signal is true:
```ts
const showCount = signal(false);
const count = signal(0);
const conditionalCout = computed(() => {
    if(showCount()){
        return `the count is ${count()}.`;
    } else {
        return `Nothing to see here!`;
    }
});
```
when you read `conditionalCount`, if `showCount` is `false` the "Nothing to see here!" message is returned *without* reading the `count` signal. this means that if you later update `count` it will *not* result in a recomputation of `conditionalCount`.

if you set `showCount` to `true` and then read `conditionalCount` again, the derivation will re-execute and take the branch where `showCount` is `true` returning the message which shows the value of `count`. changing `count` will then invalidate `conditionalCount`'s cached value.

note that dependencies can be removed during a derivation as well as added. if you later set `showCount` back to `false`, then `count` will no longer be considered a dependency of `conditionalCount`.


### Reading signals in `OnPush` components
when you read a signal within an `OnPush` component's template, Angular tracks the signal as a dependency of that component. when the value of that signal changes, Angular automatically [**marks**](https://angular.dev/api/core/ChangeDetectorRef#markforcheck) the component to ensure it gets updated the next time change detection runs.
refer to the [**skipping component subtrees**](https://angular.dev/best-practices/skipping-subtrees) guide for more information about `OnPush` components.

## Effects
Signals are useful because they notify interested consumers when they change. An **effect** is an operation that runs whenever one or more signal values change. You can create an effect with the `effect` function:
```ts
effect(() => {
    console.log(`The current count is: ${count()}`);
});
```
effects always run **at least once**. when an effect runs it tracks any signal value reads. Whenever any of these signal values change the effect runs again. Similar to computed signals effects keep track of their dependencies dynamically and only track signals which were read in the mose recent execution.

effects alrays execute **asynchronously**, during the change detection process.

### Use cases for effects
effects are rarely needed in most application code but may be useful in specific circumstances. Here are some examples of situations where an `effect` might be a good solution:
- Logging data being displayed and when it changes, either for analytics or as a debugging tool.
- Keeping data in sync with `windows.localStorage`.
- Adding cumtom DOM behavior that can't be expressed with template syntax.
- Performing custom rendering to a `<canvas>`, charting library or other third party UI library.
---
> when not to use effects
avoid using effects for propagation of state changes. this can result in `ExpressionChangedAfterItHasBeenChecked` error, infinite circular updates or unnecessary change detection cycles.
Instead, use `computed` signals to model state that depends on other state.
---

### Injection context
by default, you can only create an `effect()` within an [injection context](https://angular.dev/guide/di/dependency-injection-context) (where you have access to the `inject` function). The easiest way to satisfy this requirement is to call `effect` within a component, directive or service `constructor`:
```ts
@Component({ ... })
export class EffectiveCounterComponent {
    readonly count = signal(0);
    counstructor(){
        // register a new effect.
        effect(() => {
            console.log(`the count is ${this.count()}`);
        });
    }
}
```
alternatively, you can assign the effect to a field (which also gives it a descriptive name).
```ts
@Component({ ... })
export class EffectiveCounterComponent {
    readonly count = signal(0);

    private loggingEffect = effect(() => {
        console.log(`ten count is ${this.count()}`);
    });
}
```
to create an effect outside the constructor, you can pass an `Injector` to `effect` via its options:
```ts
@Component({ ... })
export class EffectiveCounterComponent {
    readonly count = signal(0);
    private injector = inject(Injector);

    initializeLogging(): void {
        effect(() => {
            console.log(`this count is ${this.count()}`);
        },{injector: this.injector});
    }
}
```

### Destroying effects
when you create an effect, it is automatically destroyed when its enclosing context is destroyed. This means that effects created within components are destroyed when the compoent is destroyed. The same goes for effects within directives, services, etc.

effects return an `EffectRef` that you can use to destroy them manually by calling the `.destroy()` method. You can combine this with the `manualCleanup` option to create an effect that lasts until it is manually destroyed. be careful to actually clean up such effects when they're on longer required.


# Adbanced topics

## Signal equality function
when creation a signal, you can optionally provide an equality function, which will be used to check whether the new value is actually different than the previous one.
```ts
import _ from 'lodash';

const data = signal(['test'], {equal: _.isEqual});

// even though this is a different array instance, the deep equality
// function will consider the values to be equal, and the signal won't trigger any updates.
date.set(['test']);
```
equality functions can be provided to both writable and computed signals.
> **HELPFUL:** by default, signals use referential equality ([]`Object.is()`](https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Object/is) comparison).


### reading without tracking dependencies
rarely, you may want to execute code which may read signals within a reactive function such as `computed` or `effect` without creating a dependency.

for example, suppose that when `currentUser` changes, the value of a `counter` should be logged. you could create an `effect` which reads both signals:
```ts
effect(() => {
    console.log(`user set to ${currentUser()} and the counter is ${counter()}`);
});
```
this example will log a message when either `currentUser` or `counter` changes. However, if the effect should only run when `currentUser` changes, then the read of `counter` is only incidental and changes to `counter` should't log a new message.

you can prevent a signal read from begin tracked by calling its getter with `untracked`:
```ts
effect(() => {
    console.log(`user set to ${currentUser()} and the counter is ${untracked(counter)}`);
});
```
`untracked` is also useful when an effect needs to invoke some external code which shouldn't be treated as a dependency:
```ts
effect(() => {
    const user = currentUser();

    untracked(() => {
        // if the `loggingService` reads signals, they won't be counted as dependencies of this effect.
        this.loggingService.log(`User set to ${user}`);
    });
});
```

## Effect cleanup functions
effects might start long-running operations, which you should cancel if the effect is destroyed or runs again before the first operation finished. when you create an effect, your function can optionally accept an `onCleanup` function as its first parameter. this `onCleanup` function lets you register a callback taht is invoked before the next run of th effect begins or when the effect is destroyed.
```ts
effect((onCleanuo) => {
    const user = currentUser();

    const timer = setTimeout(() => {
        console.log(`1 second ago, the user became ${user}`);
    }, 1000);

    onCleanup(() => {
        cleanTimeout(timer);
    })
})
```

## Using signals with RxJS

see [RxJS interop with Angular signals](https://angular.dev/ecosystem/rxjs-interop) for details on interoperability between signals and RxJS.


# [Dependent state with `linkedSignal`](https://angular.dev/guide/signals/linked-signal)



# [Async reactivity with resources](https://angular.dev/guide/signals/resource)
> IMPORTANT: `resource` is [experimental](https://angular.dev/reference/releases#experimental). it's ready for you to try, but it might change before it is stable.
most signal APIs are synchronous -- `signal`,`computed`,`input`, etc. However applications often need to deal with data that is available asynchronously. a `Resource` gives you a way to incorporate async data into your application's signal-based code.

you can use a `Resource` to perform any kind of async operation but the moset commone use-case for `Resource` is fetching data from a server. The following example creates a resources to fetch some user data.

the easiest way to create a `Resource` is th `resource` function.
```ts
import { resource, Signal } from '@sngular/core';

const userId: Signal<string> = getUserId();

const userResource = resource({
    // define a reactive request computation.
    // the request value recomputeds whenever any read signals change.
    request: () => ({id: userId()}),
    // define an async loader that retrieves data.
    // the resource calls this function every time the `request` value changes.
    loader: ({request}) => fetchUser(request)
});

// create a computed signal bases on the result of the resources's loader function.
const firstName = computed(() => userResource.value().firstName);
```
the `resource` function accepts a `ResourceOptions` object with two main properties: `request` and `loader`.

the `request` property defines a reactive computation that produce a request value. whenever signals read in this computation change, the resource produces a new request value similar to `computed`.

the `loader` property defines a `ResourceLoader` -- an async function that retrieves come state. the resource calls the loader every time the `request` computation produces a new value passing the value to the loader. set [Resource loaders](https://angular.dev/guide/signals/resource#resource-loaders) below for more details.

`Resource` has a `value` signal that contains the results of the loader.


## resource loaders
when creating a resource, you specify a `ResourceLoader`. this loader is an async function that accepts a single parameter -- a `ResourceLoaderParams` ojbect -- and returns value.

The `ResourceLoaderParams` object contains three properties: `request`, `previous` and `abortSignal`.
| Property | Description |
| -------- | ----------- |
| `request`| the value of the resource's `request` computation. |
| `previous` | an object with a `status` property, containing the previous `ResourceStatus`. |
| `abortSignal` | an [`AbortSignal`](https://developer.mozilla.org/en-US/docs/Web/API/AbortSignal). ses [Aborting requests](https://angular.dev/guide/signals/resource#aborting-requests) below for details |
if the `request` computation returns `undefined`, the loader function does not run and the resource status becomes `Idle`.


## Aborting requests



## Reloading


# Resource status





---
---
***
***
___
___

# [Signals playground](https://angular.dev/playground#Signals)
```ts
import { Component, signal, computed } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';



@Component({
    selector: 'app-root',
    template: ` `
})
export class AppComponent {
    // signal
    count = signal(10);

    butter = computed(() => this.count() * 0.1);
    sugar  = computed(() => this.count() * 0.05);
    flour  = computed(() => this.count() * 0.2);

    update(event: Event){
        const input = event.target as HTMLInputElement;
        this.count.set(parseInt(input.value));
    }

    // control flow
    todos: Array<{done: boolean; text: string}> = [];

    add(text: string){
        this.todos.push({text, done: false});
    }

    toggle(index, number){
        this.todos[index].done = !this.todos[index].done;
    }

 }

bootstrapApplication(AppComponent);
```

