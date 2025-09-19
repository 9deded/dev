# [Forms in Angular](https://angular.dev/guide/forms)
> handing user input with forms in the cornerstone of many common applications.

applications use forms to enable users to log in, to update a profile, to enter sensitive information, and to perform many other data-entry tasks.

Angular provides two different approaches to handling user input through forms: reactive and template-driven. both capture user input event from the view, validate the user input, create a form model and data model to update and provide a way to track changes.

this guide provides information to help you decide which type of form works best for your situation. it introduces the common building blocks used by both approaches. it also summarizes the key differences between the two approaches, and demonstrates those differences in the context of setup, data flow, and testing.

### Chooshing an approach
reactive forms and template-driven forms process and manage form data differently. each approach offers different advantages.
| Forms | Details |
| ----- | ------- |
| reactive forms | Provide direct, explicit access to the underlying form's object model. compared to template-driven forms, they are more robust: they're more scalable, reusable, and testable. if forms are a key part of your application, or you're already using reactive patterns for building your application, use reactive forms. |
| Template-driven forms | Rely on directives in the template to create and manipulate the underlying object model. they are useful for adding a simple form to an app, such as an email list signup form. they're straightforward to add to an app, but they don't scale as well as reactive forms. if you have very basic form requirements and logic that can be managed solely in the template, template-driven forms colud be a good fit. |

### Key differences
the following table summarizes the key differences between reactive and template-driven forms.
|       | Reactive | Template-driven |
| ----- | -------- | --------------- |
| [Setup of form model](#setup-of-form-model) | explicit, created in component class | implicit, created by directive |
| [Data model](#data-model) | structured and immutable | unstructured and mutable |
| [Data flow](#data-flow) | synchronous | asynchronous |
| [Form validation](#form-validation) | functions | directives |


### Scalability
if forms are a central part of your application, scalability is very important. being able to reuse form models across components is critical.

reactive forms are more scalable then template-driven forms. they provide direct access to the underlying form API, and use [synchronous data flow](#data-flow-in-reactive-forms) between the view and the data model, which makes creating large-scal forms easier. reactive forms require less setup for testing, and testing does not require deeo understanding of change detection to property test form updates and validation.

templete-driven forms focus on simple scenarios and are not as reusable. they abstract away the underlying form API, and use [asynchronous data flow](#sata-flow-in-template-driven-forms) between the view and the data model. the abstraction of template-driven forms also affects testing. tests are deeply reliant on manual change detection execution to run properly, and require more setup.


### Setting up the form model
both reactive and template-driven form track value changes between the form input elements that users interact with and the form data in your component model. the two approaches share underlying building block, but differ in how you create and manage the common form-control instances.


### Common form foundation classes
both reactive and template-driven forms are built on the following base classes.
| Base classes | Details |
| ------------ | ------- |
| `FormControl` | tracks the value and validation status of an individual form control. |
| `FormGroup` | tracks the same values and status for a collection of form controls. |
| `FormArray` | tracks the same values and status for an array of form controls. |
| `ControlValueAccessor` | creates a brifge between Angular `FormControl` instances and build-in DOM elements. |


### Setup in reactive forms
with reactive forms, you define the form model directly in the component class. the `[formControl]` directive links the explicitly create `FormControl` instance to a specific form element in the view, using an internal value accessor.

the following component implement an input field for a single control, using reactive forms. 
in this example, the form meodel is the `FormControl` instance.
```ts
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-reactive-favorite-color',
    template: `
        Favorite color: <input type="text" [formControl]="favoriteColorControl">
    `,
    imports: [ ReactiveFormsModule ]
})
export class FavoriteColorReactiveComponent {
    favoriteColorControl = new FormControl('');
}
```
> IMPORTANT: in reactive forms, the form model is the source of truth; ti provides the value and status of the form element at any given point in time, throught the `[formControl]` directive on the `<input>` element.

### Setup in template-driven forms
in template-driven forms, the form model is implicit, rather than explicit. the directive `NgModel` creates and manages a `FormControl` instance for a given form element.

the following component implements the same input field for a single control, using template-driven forms.
```ts
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-template-favorite-color',
    template: `
        Favorite color: <input type="text" [(ngModel)]="favoriteColor"/>
    `,
    imports: [FormsModeule]
})
export class FavoriteColorTemplateComponent {
    favoriteColor = '';
}

```
> IMPORTANT: in a template-driven form the source of truth is the template. the `NgModel` directive automatically manages the `FormControl` instance for you.

### Data flow in forms
when an application contains a form, Angular must keep the view in sync with the component model and the component model in sync with the view. as users change values and make selections through the view, the new value must be reflected in the data model. similarly, when the program logic changes values in the data model, those values must be reflected in the view.

reactive and template-driven forms differ in hom they handle data flowing from the user or from programmatic changes. the following diagrams illustrate both kinds of data flow for each type of form, using the favorite-color input field defined ablove.


### Data flow in reactive forms
in reactive forms each form element in the view is directly linked to the form model (a `FormControl` instance). updates from the view to the mmodel and form the model to the view are synchronous and do not depend on how the UI is rendered.

the view-to-model diagram shows how data flows when an input field's value is changed fromthe view throught the following steps.
1. the user types a value into the input element, in this case the favorite color ***Blue***.
2. the form input element emits an "input" event with the latest value.
3. the `ControlValueAccess` listening for events on the form input element immediately relays the new value to the `FormControl` instance.
4. the `FormControl` instance emits the new value through the `valueChanges` observable.
5. any subscribers to the `valueChanges` observable receive the new value.

[  ]

the model-to-view diagram show how a programmatic change to the model is progagate to the view through the following ste.
1. thie user calls the `favoriteColorControl.setValue()` method, which updates the `FormControl` value.
2. the `FormControl` instance emits the new value through the `valueChanges` observable.
3. any subscribers to the `valueChanges` observable receive the new value.
4. the control value accessor on the form input element updates the element with the new value.



### Data flow in template-driven forms
in template-driven forms, each form element is linked to a directive that manages the form model internally.

the view-to-model diagram shows how data flows when an input field's value is changed from the view through the following steps.
1.





```html
<mat-form-field appearance="fill">
    <mat-label>Input label</mat-label>
    <input matInput (click)="clickInput($event)" (click)="clickInput($event.target)" />
</mat-form-field>
```
```ts
import { Component } from '@angular/core';

@Component({...})
export class MyComponent {
    clickInput(event: FocusEvent):void {
        const inputElement = event.target as HTMLInputElement;
        inputElement.select();
    }
    clickInput(inputElement: HTMLInputElement):void{
        inputElement.select();
    }
}
```