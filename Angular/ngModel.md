
# [NgModel](https://angular.dev/api/forms/NgModel)
creates a `FormControl` instance from a domain model and binds in to a form control element.

### `input`
one-way binging to `ngModel` wiht `[]` syntax, changing the domain model's value in the component class sets the value in the view.
two-way binding with `[()]` syntac (also known as 'banana-in-a-box syntax'), the value in the UI always syncs back to the domain model in your class.

### `FormControl`
directive into a local templated variable using `ngModel` as the key(ex: `#myValue="ngModel"`). access the control uisng th directive's `control` property. (like `valid` and `dirty`) also exist on the control for direct access.


#### `AbstractControlDirective`

#### `FormsModels`


### Using `ngModel` on a standalone control
```ts
import { Component } from '@angular/core';

@Component({
    selector: 'example-app',
    template: `
        <input [(ngModel)]="name" #ctrl="ngModel" required />

        <p>value: {{name}}</p>
        <p>valid: {{ctrl.valid}}</p>

        <button (click)="setValue()">Set value</button>
    `,
    standalone: false,
})
export class SimpleNgModelComp {
    name:string = '';

    setValue(){
        this.name = 'Nancy';
    }
}
```

### Using ngModel within form
using the `ngModel` within `<form>` tags, it need to supply a `name` attribute so that the control can be registered with the parent form under that name.
Access tis properties by exporting it into a local template variable using `ngForm` such as (`#f="ngForm"`). use the variable where needed on form submission.
```ts
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'example-app',
    template: `
        <form #f="ngForm" (ngSubmit)="onSubmit(f)" novalidate>
         <input name="first" ngModel required #first="ngModel" />
         <input name="last" ngModel />
         <button>Submit</button>
        </form>

        <p>Firstname value: {{first.value}}</p>
        <p>Firstname valid: {{first.valid}}</p>
        <p>Form value: {{ f.value | json }}</p>
        <p>Form valid: {{ f.valid }}</p>
    `,
    standalon: false
})
export class SimpleFormComp{
    onSubmit(f: NgForm){
        console.log(f.value);
        console.log(f.valid); 
    }
}
```

### Using a standalone ngModel within a group
use a standalone ngModel control within a form. This controls the display of the form, but doesn't contain form data.
```html
<form>
    <input name="login" ngModel placeholder="login" />
    <input type="checkbox" ngModel [ngModelOptions]="{standalone:true}" />Show more options?
</form>


<!-- setting the ngModel `name` attribute through options -->
<form>
    <my-custom-form-control name="Nancy" ngModel [ngModelOptions]="{name:'user'}"></my-custom-form-control>
</form>
<!-- form value: {login: ''} -->
 ```





=====

[angular model](https://www.w3schools.com/angular/angular_model.asp)
- ng-empty
- ng-not-empty
- ng-touched
- ng-untouched
- ng-valid
- ng-invalid
- ng-dirty
- ng-pending
- ng-pristine
