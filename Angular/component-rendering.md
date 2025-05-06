# [Programmatically rendering components](https://angular.dev/guide/components/programmatic-rendering)
in addition to using a component directly in a template, you can also dynamically render components. There are two main ways to dynamically render a compoent: in a template with `NgComponentOutlet`, or in your TypeScript code with `ViewContainerRef`.

### Using NgComponentOutlet
`NgComponentOutlet` is a structural directive that dynamically renders a given component in a template.

```ts
@Component({ ... })
export class AdminBio { /* ... */ }

@Component({ ... })
export class StandardBio { /* ... */ }

@Component({
    ...,
    template: `
        <p>Profile for {{user.name}}</p>
        <ng-container *ngComponentOutlet="getBioComponent()" />
    `
})
export class CustomDialog {
    @Input() user: User;

    getBioComponent(){
        return this.user.isAdmin ? AdminBio: StandardBio;
    }
}
```
see the [NgComponentOutlet API reference](https://angular.dev/api/common/NgComponentOutlet) for more information on the directive's capabilities.


### Using ViewContainerRef
a **view container** is a node in angular's component tree that can contain content. Any component or directive can inject `ViewContainerRef` to get a reference to a view container corresponding to that component or directive's location in the DOM.

can use the `createComponent` method on `ViewContainerRef` to dynamicaaly create and render a component. When you create a new component with a `ViewcontainerRef`, Angular appends it into the DOM as the new sibling of the component or directive that injected the `ViewContainerRef`.
```ts
@Component({
    selector: 'leaf-content',
    template: ` this is the leaf content`
})
export class LeafContent {}


@Component({
    selector: 'outer-container',
    template: `
        <p>This is the start of the outer container</p>
        <inner-item />
        <p>This is the end of the outer container</p>
    `
})
export class OuterContainer {}


@Component({
    selector: 'inner-item',
    template: `<button (click)="loadContent()">Load content</button>`
})
export class InnerIten{
    private viewContainer = inject(ViewContainerRef);

    loadContent(){
        this.viewContainer.createComponent(LeafContent);
    }
}
```
in the example above, cliecking the "Load content" button results in the following DOM structure
```html
<outer-container>
    <p>This is the start of the outer container</p>
    <inter-item>
        <button>Load content</button>
    </inner-item>
    <leaft-content>This is the leaf content</leaf-content>
    <p>This is the end of the outer container</p>
</outer-container>
```

### Lasy-loading components
you can use both of the approaches described above `NgComponentOutlet` and `ViewContainerRef` to render components that are lazy-loaded with a standard JavaScript [dynamic import](https://developer.mozilla.org/docs/Web/JavaScript/Reference/Operators/import)
```ts
@Component({
    ...,
    template: `
        <section>
            <h2>Basic settings</h2>
            <basic-settings />
        </section>
        <section>
            <h2>Advanced setting</h2>
            <button (click)="loadAdvanced()" *ngIf="!admancedSettings"> Load advanced settings </button>
            <ng-container *ngComponentOutlet="advancedSettings" />
        </sction>
    `
})
export class AdminSettings{
    advancedSettings: { new (): AdvancedSettings } | undefined;

    async loadAdvanced() {
        const { AdvancedSettnigs } = await import('path/to/advanced_seggins.js');
        this.advancedSettings = AdvancedSettings;
    }
}
```
the example above loads and display the `AdvancedSettings` upon receiving a button click.