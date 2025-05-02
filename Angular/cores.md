









## [Input](https://angular.dev/api/core/Input)
Decorator that marks a class field as an input property and supplies configuration metadata. The input property is bound to a DOM property in the template. During change detection, Angular automatically updates the data property with the DOM property's value.

// @Input = property


***API***
```ts
@Input ({
    alias?: string|undefined; // the name of the DOM property to which the input property is bound.
    required?: boolean|undefined; // whether the input is required for the directive to function.
    transform?: ((value:any) => any)|undefined; // function with which to transform the input value before assigning it to the directive instance.
})
```

***Usage Notes***
```ts
import { Component, Input, numberAttribute, booleanAttribute } from '@angular/core';

@Component({
    selector: 'bank-account',
    template: `
        Bank Name: {{bankName}},
        Account Id: {{Id}}
        Account Status: {{status ? 'Active':'InActive'}}
    `
})
class BankAccount {
    // this property is bound using its original name.
    // defining argument required as true inside the Input Decorator
    // makes this property deceleration as mandatory
    @Input({ required:true}) bankName!:string;
    // argument alias makes this property value is bound to a different property name
    // when this component is instantiated in a template.
    // argument transform convert the input value from string to number.
    @Input({ alia:'axxount-id', transform: numberAttribute }) id: number;
    // argument transform the input value from string to boolean
    @Input({ transform: booleanAttribute }) status:boolean;
    // this property is not bound, and is not automatically updated by Angular
    normalizedBankName: string;
}



@Component({
    selector: 'app',
    template: `<bank-account bankName="RBC" account-id="4444" status="true"></bank-account>`
})
class App {}
```


```ts
<section>
    <app-housing-location [housingLocation]="housingLocation" [attribute]="value"></app-housing-location>
</section>
```

using the `{{ expression }}` in Angular templates, you can render values from properties, `Inputs` and valid JavaScript expressions.
```html
<section class="listing">
    <img class="listing-photo" [src]="housingLocation.photo" alt="Exterior photo of {{housingLocation.name}}" crossorigin />
    <h2 class="listing-heading">{{ housingLocation.name }}</h2>
    <p class="listing-location">{{ housingLocation.city }}, {{ housingLocation.state }}</p>
</section>
```