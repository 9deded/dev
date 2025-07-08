






### [of()](https://www.learnrxjs.io/learn-rxjs/operators/creation/of)
the `of` operator in RxJS is used to create an Observable that emits a sequence of provided vlaues one at a time and them completes.
it's a simple way to turn static data into an observable stream.
```ts
import { of } from 'rxjs';

const source = of('hello', [1,2,3], { name: 'RxJS' });

source.subcribe({
    next: (value) => console.log(value),
    complete: () => console.log('complete')
});
```

