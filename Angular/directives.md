# [Directives](https://angular.dev/guide/directives)
***Built-in directives***
>Directives are classes that add additional behavior to elements in your Angular applications.
use Angular's built-in directives to manage forms, lists, styles and what users see.

the defferent types of Angular directives are as follows:
| Directive Types | Details |
| --------------- | ------- |
| [Components](https://angular.dev/guide/components) | used with a template. this type of directive is the most commone directive type. |
| [Attribute directives](#built-in-attribute-directives) | change the apperance or behavior of an element, components or another directive. |
| [Structural directives](#built-in-structural-directives) | change the DOM layout by adding and removing DOM elements. |
this guide covers built-in [attribute directives](#built-in-attribute-directives) and [structural directives](#built-in-structural-directives).



## Built-in attribute directives
attribute directives listen to and modify the behavior of other HTML elements, attributes, properties and components.

the momst commone attribute directives are as follows:
| Commone directives | Details |
| ------------------ | ------- |
| `NgClass` | adds and removes a set of CSS classes. |
| `NgStyle` | adds and removes a set of HTML styles. |
| `NgModel` | adds two-way data binging to an HTML form element. |
> HELPFUL: built-in directives use only public APIs. they do not have special access to any private APIs that other directives can't access.

## Adding and removing classes with `NgClass`
add or remove multiple CSS classes simultaneously with `ngClass`.
> HELPFUL: to add or remove a ***single class***, use [class binding](https://angular.dev/guide/templates/class-binding) rather than `NgClass`.


### import `NgClass` in the component
to use `NgClass`, add it to the component's `imports` list.
```ts
import { NgClass } from '@angular/common';
...
@Component({
    ...
    NgClass, // <-- import into the component
    ...
})
export class AppComponent implement OnInit{
    ...
}
```


### using `NgClass` with an expression
on the element you'd like to style, add `[ngClass]` and set it equal to an expression. in this case, `inSpecial` is a boolean set to `true` in `app.component.ts`. because `isSpecial` is true, `ngClass` applies the class of `special` to the `<div>`.
```html
<-- toggle the "special" class on/off with a proprty -->
<div [ngClass]="isSpecial ? 'special' : ''">This div is special</div>
```


### using `NgClass` with a method
1. to use `NgClass` with a method, add the method to the component class. in the following example, `setCurrentClasses()` sets the property `currentClasses` with an object that adds or removes three classes based on the `true` or `false` state of three other component properties.

each key of the object is a CSS class name. if a key is `true`, `ngClass` adds the class. if a key is `false`, `ngClass` removes the class.
```ts
currentClasses: Record<string, boolean> = {};
...
setCurrentClasses(){
    // css classes: added/removed per current state of component properties
    this.currentClasses = {
        saveable: this.canSave,
        modified: !this.inUnchanged,
        special: this.isSpecial
    }
}
```

