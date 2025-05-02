


### [Styling](https://angular.dev/guide/components/styling)
```ts
import { Component } from '@angular/core';

@Component({
    selector: 'profile-photo',
    template: `<img src="profile-photo.jpg" alt="profile photo">`,
    styles: `img P border-radius: 50%;`,
    encapsulation: ViewEncapsulation.None, // Enulated | ShadowDom | None
    templateUrl: 'profile-photo.html',
    stuleUrl: 'profile-photo.css'
})
export class ProfilePhoto {}
```


### [input properties](https://angular.dev/guide/components/inputs)
```ts
import { Component, input } from '@angular/core';

@Component({/*...*/})
export class CustomSlider {
    // declare an input named 'value' with a default value of zero.

    // typescript infers that is input in a number, returning InputSignal<number>.
    value = input(0);
    
    // produces an InputSignal<number|undefined> because `value` may not be set.
    value = input<number>();
}
```
this lets you bind to the property in a template:
```html
<custom-slider [value]="50" />
```


### Reading inputs
the `input` function returns an `InputSignal`. You can read the value by calling the signal:

```ts
import { Component, input } from '@angular/core';

@Component({/*...*/})
export class CustomSlider {
    // declare an input named 'value' with a default value of zero.
    value = input(0);

    // create a computed expression that reads the value input
    label = computed(() => `The slider's value is ${this.value()}`);
}
```

### Required inputs
declare an input is `required` by calling `input.required` instead of `input`:
```ts
@Component({/*...*/})
export class CustomSlider {
    // declare a required input named value. returns an `InputSignal<number>`.
    value = input.required<number>();
}
```
Angular enforces that required input --must-- be set when the component is used in a template. if you try to use a component without specifying all of its required inputs, Angular reports an error at build-time.

Required inputs do not automatically include `undefined` in the generic parameter of the returned `InputSignal`.


### Configuring inputs
The `input` function accepts a config object as a second parameter that lets you change the way that input works.

#### input transforms
a `transform` function to change the value of an input when it's set by Angular.

```ts
@Component({
    selector: 'custom-slider',
    /*...*/
})
export class CustomSlider {
    label = input('', { transform: trimString });
}

function trimString(value: string| undefined): string{
    return value?.trim() ?? '';
}
```
```html
<custom-slider [label]="systemValue" />
```