# [Custom Elements](https://angular.dev/guide/elements)


### Using custom elements
add the `@angular/elements` package to your workspace, run the following command:
```ts
$ npm install @angular/elements --save
```

### How it works
`createCustomElement()` function converts a component into a class the can be registered with the browser as a customer element. After you register your configured class with the browser's coustom-element registry, user the new element just like a built-in HTML element in content that you add directliy into the DOM:
```html
<my-popup message="User Angular!"></my-popup>
```
when your custom element is placed on a page, the browser creates an instance of the registered class and adds it to the DOM. the content is provided by the compoents's template, which uses Angular template systax, and is rendered using the component and DOM data. Input properties in the component correspond to input attributes for the element.

### Transforming compoent s to custom elements
Angular provides the `createCustomElement()` function for converting an Angular component, together with its dependencies, to a custom element.

The conversion process implements the `NgElementConstructor` interface and creates a constructor class that is configured to produce a self-bootstrapping instance of your component.

Use the browse's native [`customElements.define()`](https://developer.mozilla.org/docs/Web/API/CustomElementRegistry/define) function to register the configured constructor and its associated custom-element tag with the browser's [`CustomElementRegister`](https://developer.mozilla.org/en-US/docs/Web/API/CustomElementRegistry). When the browser encounters the tag for the registered element, it uses the constructor to create a custom-element instance.


## Mapping