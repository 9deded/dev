# [NgModules](https://angular.dev/guide/ngmodules/overview)
> IMPORTANT: the Angular team recommends using standalone components instead of `NgModule` for all new code. using this guide to understand existing code built with `NgModule`.

an NgModule is a class baked by the `@NgModule` decorator. this decorator accepts metadata that tells Angular how to compile component templates and configure dependency injection.
```ts
import { NgModule } from '@angular/core';

@NgModule({
    // metadata goes here
})
export class CustomMenuModule {}
```
an NgModule has two main responseibilities:
- declaring components, directives and pipes that belong to the NgModule.
- add providers to the injector for components, directives and pipes that import the NgModule


## Declarations
the `declarations` property of the `@NgModule` metadata declares the components,directives and pipes that belong to the NgModule.
```ts
@NgModule({
    // CustomeMenu and CustomMenuItem are components.
    declarations: [ CustomMenu, CustomMenuItem ]
})
export class CustomMenuModule {}
```
in the example above, the components `CustomMenu` and `CustomMenuItem` belong to `CustomMenuModule`.

the `declarations` property additionally accepts arrays of components, directives and pipes. these arrays in turn may also contain other arrays.
```ts
const MENU_COMPONENTS = [ CustomMenu, CustomMenuItem ];
const WIDGETS = [ MENU_COMPONENTS, CustomSlider ];

@NgModule({
    // this NgModule declares all of CustomMenu, CustomMenuItem, CustomSlider and CustomCheckbox.
    declarations: [ WIDGETS, CustomCheckbox ]
})
export class CustomMenuModule {}
```
if Angular discovers any components, directives or pipes declared in more than one NgModule, it reports an error.

any components, directives or pipes must be explicitly marked as `standalone: false` in order to be declared in an NgModule.
```ts
@Component({
    // mark this component as `standalone: false` so that it can be declared in an NgModule.
    standalone: false,
    // ...
})
export class CustomMenu { /* ... */ }
```

## imports 
components declared in an NgModule may depend on other components 
