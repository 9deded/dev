






### Angular components
angular apps are build around components, which are angular's building blocks. Components contain the code, HTML layout and CSS style information that provide the function and appearance of an element in the app. In Angular, components can contain other components. An app's functions and appearance can be divided and partitioned into components.

In Angular, components have metadata that define its properties. When you create your `HomeComponent`, you use these properties:
- `selector`: to describe how Angular refers to the component in templates.
- `standalone`: to describe whether the component requires a `NgModule`.
- `imports`: to describe the component's dependencies.
- `template`: to describe the component's HTML markup and layout.
- `styleUrls`: to list the URLs of the CSS files that the component uses in an array.












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


### pass parameters to Angular components
##### 1. using `@Input()` Decorator
- the most common way to pass data from a parent component to a child component is using the `@Input()` decorator.
- in the child component, decorate a property with `@Input()`. this makes the property available for binding in the parent component's template.
```ts
// child component child.component.ts
imort { Component, Input } from '@angular/core';

@Component({
    selector: 'app-child',
    template: '<div>{{ message }}</div>`
})
export class ChildComponent {
    @Input() message: string = "";
}
```
```html
<!-- parent component template -->
 <app-child message="Hello from parent"></app-child>
```
you can also use property binding (square brackets) to pass dynamic values.
```html
<app-child [message]="parentMessage"></app-child>
```
```ts
// parent component
export class ParentComponent {
    parentMessage: string = "Dynamic message";
}
```

#### 2. using route parameters
- for components loaded via the router, you can pass data as route parameters.
- define route parameters in your routing configuration.
```ts app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './product-detail/product-detail.component';

const routes: Routes = [{path: '/product/:id', component: ProductDetailComponent }];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
```
- in the component, use `ActivatedRoute` to access the parameter.
```ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-product-detail',
    template: '<div>Product ID: {{ productId }}</div>'
})
export class ProductDetailComponent implement OnInti {
    productId: string | null = null;

    constructor(private route: ActivatedRoute) {}

    ngOnInit(){
        this.route.paramMap.subscribe((params) => {
            this.productId = params.get('id');
        });
    }
}
```