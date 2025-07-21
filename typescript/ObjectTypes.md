# [Object Types](https://www.typescriptlang.org/docs/handbook/2/objects.html)
in Javascript, the fundamental way that we group and pass aroudn data is through objects.
in Typescript, we represent those through ***object types***.
```ts
function greet(person: { name:string; arge:number }){
    return "Hello " + person.name;
}
```
or they can be named by using either an interface:
```ts
interface Person { name:string; age:nuber; }

function greet(person:Person){
    return "Hello " + person.name;
}
```
or a type alias:
```ts
type Person = { name:string; age:number; };

function greet(person: Person){
    return "Hello " + person.name;
}
```
in all three examples above, we've written function that take objects that contain the property `name` (which must be a `string`) and `age`(which must be a `number`).

### Quick Reference
each property in an object type can specify a couple of things: the type, whether the property is optional, anme whether the property can be written to.

### Property Modifiers
each property in an ojbect type can specify a couple of things: the type, whether the property is optional and whether the property can be written to.

#### Optional Properties
much of the time, we'll find ourselves dealing with objects that might have a property set. in those cases, we can make those properties as optional by adding a questing mark (`?`) to the end of their names.
```ts
interface PaintOptions{
    shape:Shape;
    xPos?:number;
    yPos?:number;
}

function paintShape(opts: PaintOptions){ ... }

const shape = getShape();
paintShape({ shape });
printShape({ shape, xPos:100 });
paintShape({ shape, yPos:100 });
paintShape({ shape, xPos:100, yPos:100 });
```
in this example, both `xPos` and `yPos` are comsidered optional, we can choose to provide either of them, so every call above to `paintShape` is valid. all optionality really says is that if the property is set, it better have a specific type.

we can also read from these properties - but when we do under `strictNullChecks`, typescript will tell us they're potentially `undefined`.
```ts
function paintShape(opts: PaintOptions){
    let xPos = opts.xPos; // <- (property) PaintOptions.xPos?: number | undefined
    let xPos = opts.yPos; // <- (property) PaintOptions.yPos?: number | undefined
}





