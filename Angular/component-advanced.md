# [Advance conponent configuration](https://angular.dev/guide/components/advanced-configuration)

### ChangeDetectiveStrategy
the `@Conponent` decorator accepts a `changeDetection` option that controls the component's ***change detection mode***. There are two change detection mode options.

`ChangeDelectionStrategy.Default` is unsurprisingly, the default strategy. In this mode, Angular checks whether the component's DOM needs an update whenever any activity many have occurred application-wide. Activities that trigger this checking include user interaction, network response, timers and more.

`ChangeDetectionStrategy.OnPush` is an optional mode that reduces the amount of checking Angular needs to perform. In this mode, the framework only checks if a component's DOM needs an update when:
- A component input has changes as a result of a binding in a template or
- An event listener in this component runs
- The Component is explicitly marked for check via `ChangeDetectorRef.markForCheck` or something which wraps it like `AsyncPipe`.

Additionally, when a `OnPush` component is checked, Angular *also* checks all of its _ancestor components_, traversing upwards through the applicaiton tree.

### PreserveWhitespaces
by default, Angular removes and collapses superfluous whitespace in templates, most commonly from newlines and indentation. You can change this setting by explicitly setting `preserveWhitespaces` to `true` in a component's metadata.

### Custom element schemas
by default, Angular throws an error when it encounters an unknown HTML element. You can disable this behavior for a component by including `CUSTOM_ELEMENTS_SCHEMA` in the `schemas` property in your component metadata.

```ts 
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
    ...,
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    template: '<some-unknown-component></some-unknown-component>'
})
export class ComponentWithCustomElements{}
```

Angular does not support any other schemas at this time.