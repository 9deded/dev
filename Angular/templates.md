# [Templates](https://angular.dev/guide/templates/binding)

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

