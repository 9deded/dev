

Angular automatically adds attributes like _ngcontent-ng-c0 to HTML elements for style encapsulation. This mechanism, part of Angular's default ViewEncapsulation.Emulated mode, ensures that component styles don't unintentionally affect other components.

Here's how it works:
**Unique Attributes:** Angular generates a unique attribute (e.g., _ngcontent-ng-c0) for each component instance.
**Attribute Application:** This attribute is added to all elements within the component's template.
**CSS Selector Modification:** Angular modifies the CSS selectors defined in the component's styles to include this unique attribute. This ensures that the styles only apply to the elements within that specific component instance.
This approach effectively emulates Shadow DOM, isolating component styles without relying on the native browser feature.

***Key Points:***
**ViewEncapsulation Modes:**
Angular offers three view encapsulation modes:
`ViewEncapsulation.Emulated` (default): Uses the attribute-based approach described above.
`ViewEncapsulation.Native`: Uses the browser's native Shadow DOM.
`ViewEncapsulation.None`: Disables encapsulation, making styles global.

**Purpose:**
The _ngcontent attributes enable Angular to manage CSS scope and prevent style collisions between components.
**Implementation Detail:**
The specific attribute values (e.g., _ngcontent-ng-c0) are implementation details and should not be relied upon directly.

> This system ensures that component styles are well-contained and predictable, simplifying development and preventing unexpected style conflicts.

```ts
@Component({
    selector: "xxxx",
    encapsulation: ViewEncapsulation.Emulated | None | ShadowDom,
    ...
})
export class Xxxx {}
```

