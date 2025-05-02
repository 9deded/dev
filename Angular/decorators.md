# Decorators in Angular
`Angular decorators` are design patterns that add metadata to `classes, methods, properties or parameters, modifying` their behavior without altering the original code. 
They are functions prefixed with `@` and are a TypeScript feature used extensively in Angular to define `components, services, directives, pipes and modules`.

`Angular decorators` are inprotant for `defining and configuring` various `application elements`, providing an easy way to enhance the classes with additional functionality and provide metadata.

### Types of Decorators in Angular

1. **Class Decorators**: are applied to classed to modifiy their behavior or metadata. The examples include `@Component`, `@Directive` and `@NgModule`.
2. **Properti Decorators**: are applied to the class properties and are commonly used to modify the properties within the classes. For example, `@Input` decorator makes a property as an input binding, allowing it to bound to the external data.
3. **Method Decorators**: are applied to the class methods and modify their behavior or add additional functionalities. For example `@HostListener` allows us th listen for events on a method.
4. **Parameter Decorators**: are used for parameters inside class constructors. The parameter decorators provide additional information about constructor parameters. For example `@Inject` decorator allows to specify dependencies for dependency injection.


### Uses of Decorators in Angular
- **Compoent Configuration**: use `@Component` to define the metadata of Angular components, including template, styles and selector.
- **Service Definition**: mark a class with `@Injectable` to make is injectable as a service throughout the application.
- **Directive Behavior**: implement `@Directive` to attach custom behavior to elements in th DOM.
- **Pipe Transformation**: use `@Pipe` to define custom data transformation logic for templates.
- **Module Organization**: use `@NgModule` to structure Angular modules and manage dependencies.
- **Input and Outupt Handling**: use `@Input` and `@Output` to pass data between parent and child components.
- **View Management**: utilize `@ViewChild` and `@ViewChildren` to access child components or elements in the view.


[](https://www.tektutorialshub.com/angular/angular-decorators/)


> **Class decorators**
> - @NgModule
> - @Compoent
> - @Injectable
> - @Directive
> - @ptoomey

> **Property Decorators**
> - @Input
> - @Output
> - @ContentChild & ContentChildren
> - @ViewChild & @ViewChildren
> - @HostBinding

> **Method decorators**
> - @HostListener

> **Parameter decorators**
> - @Inject
> - @Host
> - @Self
> - @SkipSelf
> - @Optional
