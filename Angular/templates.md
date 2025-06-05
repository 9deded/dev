# [Templates](https://angular.dev/guide/templates/binding)
in Angular, a **binding** creates a dynamic connection between a component's template and its data
this connection ensures that changes to the component's data automatically update the rendered template.

## render dynamic text with text interpolation
you can bind dynamic text in templates with double curly brances, which tells Angular that it is responsible for the expression inside and ensuring it is updated currently.
thsi is called **text interpolation**.

```ts
@Component({
    template: `<p>your color preference is {{theme}}.</p>`
})
export class XComponent { theme = 'dark'; }
```
in this example, when the snippet is rendered to the page, Angular will replace `{{theme}}` with `dark`.
```html
<!-- rendered output -->
<p>your color preference is dark.</p>
 ```
 all expression values are converted to a string. **objects** and **array** are converted using the value's `toString` method.


## binding dynamic properties and attributes
Angular supports binding dynamic values into ojbect properties and HTML attributes with square brackets.

you can bind to properties on an HTML element's DOM instance,
a [component](https://angular.dev/guide/components) instance, or
a [directive](https://angular.dev/guide/directives) instance


### Native element properties
every HTML element has a corresponding DOM representation. 
for example, each `<button>` HTML element corresponds to an instance of `HTMLButtonElement` in the DOM.
in Angular, you use property bindings to set values directly to the DOM representation of the element.
```html
<!-- bind the `disabled` property on the button element's DOM object -->
 <button [disabled]="isFormValid">Save</button>
```
in this example, every time `isFormValid` changes, 
Angular automatically set the `disabled` property of the `HTMLButtonElement` instance.

### Component and directive properties
ehen an element is an Angular component, you can use property bindings to set component input properties using the same square bracket syntax.
```html
<!-- bind the `value` property on the `MyListbox` component instance. -->
 <my-listbox [value]="selection">
```
in this example, every time `mySelection` changes, Angular automatically sets the `value` property of the `selection` instance.

you can bind to directive properties as well.
```html
<!-- bind to the `ngSrc` property of the `NgOptimizedImage` directive -->
 <img [ngSrc]="profilePhotoUrl" alt="the current user's profile photo">
```

### Attributes
when you need to set HTML attributes that do not have corresponding DOM properties,
such ass ARIA attributes or SVG attributes to elements in your template with the `attr.` prefix.
```html
<!-- bind the `role` attribute on the `<ul>` element to the component's `listRole` property. -->
<ul [attr.role]="listRole">
```







[] = ? oneway:setProperty
() = function()
[()] = two ways































[Adding event listeners](https://angular.dev/guide/templates/event-listeners)
```ts
@Component({
    template: `
        <input type="text" (keyup)="updateField()" />

        <input type="text" (keyup)="updateField($event) />

        <!-- matches shift and enter -->
        <input type="text" (keyup.shift.enter)="updatedField($event)" />
        <!-- matches alt and left shift -->
        <input type="text" (keydown.code.alt.shiftleft)="updateField($event)" />
    `
    ...
})
export class AppComponent{
    updateField(): void {
        console.log("Field is updated!");
    }

    updateField(event: KeyboardEvent): void{
        if(event.key === 'Enter"){
            console.log('The user pressed enter in the text field.');
        }else{
            console.log(`The user pressed: ${event.key}`);
        }
        
    }
}
```


### [Deferred `@defer`](https://angular.dev/guide/templates/defer)
deferrable views, `defer` blocks, reduce the initial bundle size of your application by deferring the loading of code that is not strictly necessary for the initial rendering of a page.
this often results in a faster initial load and improvement in Core Web Vitals (CWV), primarily Largest Contentful Paint (LCP) and Time to First Byte(TTFB).

declaratively wrap a section of your template in a @defer block:

```html
<large-compoent />
```
the code for any components, directives and pipes inside the `@defer` block is split into a separate JavaScript file and loader only when necessary, after the rest of the template has been rendered.

Deferrable views support a veriety of triggers, prefetching options and sub-blocks for placeholder, loading and error state management.

## Which dependencies are deferred.
Components, directives, pipes and any component CSS styles can be deferred when loading an application.

In order for the dependencies within a `@defer` block to be deferred, they need to meet two confitions:
1. ***They must be standalone.*** Non-standalone dependencies cannot be deferred and are still eagerly loaded, even if they are inside of `@defer` blocks.
2. ***They cannot be referenced outside of `@defer` blocks within the same file.*** if they are referenced outside the `@defer` block or referenced within ViewChild queries, the dependencies with be eagerly loaded.

|    |     |
| -- | --- |
|`on`| a trigger condition using a trigger form the list of built-in triggers. for example: `@defer (on viewport)`"
|`when` | a condition as an expression which is evaluated for truthiness. When the expression in truthy, the placeholder is swapped with the lazily loaded content. for example: `@defer (when customizedConfition)`|
| -- | --- |

if the `when` condition evaluates to `false`, the `@defer` block is not reverted back to the placeholder. the swap is a one-time operation.

you can define multiple event triggers at once, these triggers will be evaluated as OR confitions.

`@defer (on viewport; on timer(2s))`
`@defer (on viewport: when customizedCondition)`

in this activity, you'll learn how to use triggers to specify the condition to load the deferrable views.


#### `on interaction` trigger

