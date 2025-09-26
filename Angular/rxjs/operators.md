## Operators
operators, even though the Observable is the foundation. Operators are the essential pieces that allow complex asynchronous code to be easily componesed in a declarative manager.

**operators are functions**. there are two kinds of operators:

**Pipeable Operators** are the kind that can be piped to Observables using the syntax `observableInstance.pipe(operator)` or more commonly `observableInstance.pipe(operatorFactory())`. Operator factory functions include, `filter(...)` and `mergeMap(...)`.
when pipeable operators are called, they do not change the existing observable instance. instead, they return a new Observable, whose subscription logic is based on the first observable.

| a pipeable operator is a function that takes an observable as its input and returns another Observable. it is a pure operation: the previous observable stays unmofified.
| a pipeable operator factory is a function that can take parameters to set the context and return a pipeable operator. the factory's arguments belong to the operator's lexical scope.
a pipeable operator is essentially a pure function which takes one observable as input and generates another observable as output. subscribing to the output observable will also subscribe to the input observable.

**Creation Operators** are the other kind of operator, which can be called as standalone functions to create a new Observable. for example: `of(1,2,3)` creates an observable that will emit 1, 2, and 3, one right after another. creation operators will be discussed in more detail in a later section.

for example, the operator called `map` is analogous to the Array method of the same name. just as `[1,2,3].map(x => x * x)` will yield `[1,4,9]`, the observable created like this:
```ts
import { of, map } from 'rxjs';

of(1,2,3)
    .pipe(map((x) => x * x))
    .subscribe((v) => console.log(`value: ${v}`));

// Logs:
// value: 1
// value: 4
// value: 9

// will emit 1,4,9. Another usseful operator is first:
import { of, first } from 'rxjs';

of(1,2,3)
    .pipe(first())
    .subscribe((v) => console.log(`value: ${v}`));
// Logs:
// value: `
```

### Piping
pipeable operators are functions, so they could be used like ordinary function: `op()(obs)` -- but in practice, there tend to be many of them convolved together and quickly become unreadable: `op4()(op3()(op2()(op1()(obs))))`. for that reason, observables have a method called `.pipe()` that accomplishes the same thing while being much easier to read:
```ts
obs.pipe(op1(), op2(), op3(), op4());
```
as a stylistic matter, `op()(obs)` is never used, even if there is only one operator; `obs.pipe(op())` is universally preferred.

#### Creation Operators
pipeable operators, creation operators are functions that can be used to create an observable with some common predefined behavior or by joining other observables.

a typical example of a creation operator would be the `interval` function. it takes a number (not an observable) as input argument, and produces an observable as output:
```ts
import { interval } from 'rxjs';

const observable = interval(1000); /* number of milliseconds */
```


### Higher-order Observables
observables most commonly emit ordinary values like strings and number, but surprisingly often, it is necessary to handle observables of observables, so-called higher-order obdervables. for example, imagine you had an observable emitting strings that were the URLs of files you wanted to see. The code might look like this:
```ts
const fileObservable = urlObservable.pipe(map((url) => http.get(url)));
```
`http.get()` returns an observable (of string or string arrays probably) for each individual URL. now you have an observable of Observables, a higher-order observable.

but how do you work with a higher-order obdervable? typically, by flaattening: by (somehow) converting a higher-order observable into an ordinary observable. from example:
```ts
const fileObservable = urlObservable.pipe(
    map((url) => http.get(url)),
    concatAll()
);
```
the `concatAll()` operator subscribes to each "inner" obdervable that comes out of the "outer" obdervalbe, and copies all the emitted values until that observable completes, and goes on the next one. all of the values are in that way conxatenated. other useful flattening operators (called `join operators`) are
- `mergeAll()` -- subscribes to each inner Observable as it arrives, then emits each value as it arrives
- `switchAll()` -- subscribes to the first inner observable when it arrives and emits each value as it arrives, but when the next inner observable arrives, unsubscribes to the provious one, and subscribes to the new one.
- `exhaustAll()` -- subscribes to the first inner observable when it arrives and emits each value as it arrives, discarding all newly arriving inner observables until that first one completes, then waits for the next inner observable.

just as many array libraries combine `map()` and `flat()` (or `flatten()`) into a single `flatMap()`, there are maping equivalents of all the RXJS flattening operator `concatMap()`, `mergeMap()`, `switchMap()` and `exhaustMap()`.





