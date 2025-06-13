


`<mat-form-field>` is a component used to wrap several **Angular Material** components and apply common 'Text field' styles such as the underline, floating label and hint messages.

```html
<mat-form-field appearance="outline">
    <mat-label>Name</mat-label>
    <input matInput />
    <mat-hint>Enter your input</mat-hint>
</mat-form-field>
```




## [Material Components Input](https://material.angular.dev/components/input/overview)
`matInput` is a directive that allow native `<input>` and `<textarea>` elements to work with `<mat-form-field>`.

```html
<form>
    <mat-form-field>
        <mat-label>Food</mat-label>
        <input matInput placeholder="ex. pizza" />
    </mat-form-field>

    <mat-form-field>
        <mat-level>Comment</mat-label>
        <textarea matINput placeholder="ex. It makes me feel ..."></textarea>
    </mat-form-field>
</form>
```

### `<input>` and `<textarea>` attributes
all of the attributes that can be used with `<input>` and `<textarea>` elements can be used on elements inside `<mat-form-field>` as well. this includes Angular directives such as `ngModel` and `formControl`.

the only limitation is that the `type` attribute can only be one of the values supported by `matNativeControl`.

### Supported `<input>` types
the following 'input types' can be used with `matNativeControl`:
- color
- date
- datetime-local
- email
- month
- number
- password
- search
- tel
- text
- time
- url 
- week

### Form field features
there are a number of `<mat-form-field>` features that can be used with any `<input matNativeControl>` or <textarea matNativeControl>`. these include error messages, hint thext, prefix, suffix andtheming. for additional information about these features, see the [form field documentation](https://material.angular.dev/components/form-field/overview).

### Placeholder
the placeholder is text shown when the `<mat-form-field>` label is floating but the input is empty. it is used to give the user an additional hint about what they should type in the input. the placeholder can be specified by setting the `placeholder` attribute on the `<input>` or `<textarea>` element. in some cases that `<mat-form-field>` may use the placeholder as the label,see the [form field label documentation](https://material.angular.dev/components/form-field/overview#floating-label).

### changing when 'error messages' are shown
the `<mat-form-field>` allows you to [associate error messages]() with your `matNativeControl`. by default, these error messages are shown when the control is invalid and the user has interacted with (touched) the element or the parent form has been submitted.
if you wish to override this behavior (e.g. to show the error as soon as the invalid control is dirty or when a parent form group is invalid), you can use the `errorStateMatcher` property of the `matNativeControl`.
the property takes an instance of an `ErrorStateMatcher` object. an `ErrorStateMatcher` must implement a single method `isErrorState` which takes the `FormControl` for this `matNativeControl` as well as the parent form and returns a boolean indicating whether errors should be shown. (`true` indicating that they should be shown and `false` indicating that they should not)
```html
<form>
    <mat-form-field>
        <mat-label>Email</mat-label>
        <input type="email" placeholder="ex. name@eamil.com" 
            matInput  [formControl]="emailFormControl" [errorStateMatcher]="matcher" />
        <mat-hint>Error appear instantly!</mat-hint>

        @if(emailFormControl.hasError('email') && !emailFormControl.hasError('required')){
            <mat-error>Please enter a valid email address</mat-error>
        }
        @if(emailFormControl.hasError('required')){
            <mat-error>Email is <strong>required</strong></mat-error>
        }
    </mat-form-field>
</form>
```
```ts
import { Component } from '@angular/core';
import { FormControl, FormGroupDirective, FormsModule, NgForm, Validators, ReactiveFormsModule } from '@angular/forms';

import { ErrorStateMatcher } from '@angular/material/core';
import { MatInportModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';


# error when invalid control is dirty, touched, or submitted.
export class MyErrorStateMatcher implements ErrorStateMatcher{
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        const isSubmitted = form && form.submitted;
        return !!(control && control.invalid && (contro.dirty || control.touched || isSubmitted));
    }
}


# @title Input with a custom ErrorStateMatcher
@Component({
    selector: 'imput-error-state-matcher-example',
    ...
    imports: [ FormModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule ]
})
export class InputErrorStateMatcherExample {
    emailFormControl = new FormControl('', [Validators.required, Validators.email]);

    matcher = new MyErrorStateMatcher();
}

```
a global error state matcher can be specified by setting the `ErrorStateMatcher` provider. this applies to all inputs. for convenience, `ShowOnDirtyErrorStateMatcher` is available in order to globally cause input errors to show when the input is dirty and invalid.
```ts
@NgModule({
    providers: [{ provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher }]
})
```

### Auto-resizing `<textarea>` elements
`<textarea>` elements can be made to automatically resize by using the `cdkTextareaAutosize` directive available in the CDK.

### Responding to changes in the autofill state of an `<input>`
the CDK provided utilities for detecting when an input becomes autofilled and changing the appearance of the autofilled state.

### Accessibility
the `matNativeControl` directive works with native `<input>` to provide an accessible experience.

### Aria attributes
if the containing `<mat-form-field> has a label it will automatically be used as the `aria-label` for the `<input>`.
however, if there's no label specified in the form field, `aria-label`, `aria-labelledby` or `<label for=...>` should be added.

### Errors and hints
any `mat-error` and `mat-hint` are automatically added to the input's `aria-describedby` list and `aria-invalid` is automatically updated based on the input's validity state.

when conveying an error, be sure to not rely solely on color. in the message itself, you can use an icon or text such as "Error:" to indicate the message is an error message.

### Troubleshooting
#### Error: Input type "..." isn't supported by matInput
this error is thrown when you attenpt to set an input's `type` property to a value that isn't supported by the `matInput` directive. if you need to use an unsupported input type with `<mat-form-field>` consider writing a custom [form field control](https://material.angular.dev/guide/creating-a-custom-form-field-control) for it.





## [Custome Form field Control](https://material.angular.dev/guide/creating-a-custom-form-field-control)
it is possible to create custom form field controls that can be used inside `<mat-form-field>`. this can be useful if you need to create a component that shares a lot of common behavior with a form field but adds some additional logic.

for example in this guide we'll learn how to create a custom input for inputting US telephone numbers and hook it up to work with `<mat-form-field>`. here is what we'll build by the end of this guide:
```html
<div [formGroup]="form">
    <mat-form-field>
        <mat-label>Phone number</mat-label>
        <example-tel-input formControlName="tel" required></example-tel-input>
        <mat-icon matSuffix>phone</mat-icon>
        <mat-hint>include area code</mat-hint>
    </mat-form>
</div>
```

```ts

```

```html example-tel-input-example.html
<html>


```

in order to learn how to build custom form field controls, let's start with a simple input component that we want to work inside the form field. for example, a phone number input that segments the parts of the number into their own input. (note: this is not intended to be a robust component, just a starting pont for us to learn.)

```ts
class MyTel {
    constructor(public area: string, public exchange: string, public subscriber; string) {}
}

@Component({
    selector: 'example-tel-input',
    styles: [`
        div { display: flex; }
        input {
            border: none;
            background: none;
            padding: 0;
            outline: none;
            font: inherit;
            text-aling: center;
            color: currentColor;
        }
    `]
    template: `
    <div role="group" [formGroup]="parts">
        <input class="area" formControlName="area" maxlength="3" />
        <span>&ndash;</span>
        <input class="exchange" formControlName="exchange" maxlength="3"/>
        <span>&ndash;</span>
        <input class="subscriber" formControlName="subscriber" maxlength="4 />
    </div>
    `
})
export class MyTelInput{
    parts: FormGroup;

    @Input()
    get value(): MyTel | null {
        let n = this.parts.value;
        if(n.area.length == 3 && n.exchange.length ==3 && n.subscriber.length == 4){
            return new MyTel(n.area, n.exchange, n.subscriber);
        }
        return null;
    }
    set value(tel: MyTel | null){
        tel = tel || new MYTel('', '', '');
        this.parts.setValue({area: tel.area, exchange: tel.exchange, subscriber: tel.subscriber});
    }

    constructor(fb: FormBuilder){
        this.parts = fb.group({
            'area': '',
            'exchange': '',
            'subscriber': ''
        });
    }
}
```

### providing our component as a MatFormFieldControl
the









### install Angular Material
add Angular Material to your application by running the following command:
```command
ng add @angular/material
```

### Display a component
```ts
import { MatSlideToggleModule } from '@angular/meterial/slide-toggle';

@Component ({
    imports: [ MatSlideToggleModule ]
})
class AppComponent {}
```
```html
<mat-slide-toggle>toggle me</mat-slide-toggle>
```
```cmd
ng serve
```


