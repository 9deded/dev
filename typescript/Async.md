
- **Async/Await**: keywords that make asynchonous code easier to write and read, handling promises more intuitively than traditional methods.
- **Promise**: a javascript object representing an asynchronouse operation's eventual completion (or failure) and its resulting value.

##### Syntax:
```ts
async function aAsyncFunction(): Promise<Type>{
    // async logic here
}
```
- async: this key word defines the function as asynchronouse, meaning it will return a promise.
- Promise<Type>: the function will return a Promise that resolves to a value of type Type. can specify the type of value that the Promise resolves to.

##### a simple asynchronous function
```ts
async function asyncFuntion(): Promise<string>{
    return "Hello, TypeScript!";
}

const a = asyncFunction();
console.log(a);
```


##### handling Asynchronous Tasks (simulating time delay)
