## [Signals](https://angular.dev/guide/signals)

Angular Signals is a system that granularly tracks how and where your state is used throughout an applicaiton, allowing the framework to optimize rendering updated.

`signal` is a wrapper around a value that notifies interested consumers when that value changes. signals can contain any value, from primitives to complex data structures.


##### Writable signals
whitable signals provide an API for updating their values directly. you create writable signals by calling the `signal function with the signal's initial value:

```ts
const count = signal(0);

// signals are getter functions - calling them reads their value.
console.log("the count is " + count());

// to change the value of a writable signal, either `.set()` it directly:
count.set(3);

// or use the `.update()` operation to compute a new value from the previous one:
// increment the count by 1.
count.update((value) => value + 1);
```

Whitable signal have the type `WritableSignal`.

##### Converting writable signals to readonly
`WritableSignal` provide a `asReadonly()` method that returns a readonly version of the signal.
```ts
@Injectable({providedIn: 'root'})
export class CounterState {
    // private writable state
    private readonly _count = signal(0);

    readonly count = this._count.asReadonly(); // public readonly

    increment(){
        this._count.update((v) => v+1);
    }
}
@Component({
    /* ... */
})
export class AwesomeCounter {
    state = inject(CounterState);

    count = this.state.count; // can read but not modify

    increment() {
        this.state.increment();
    }
}
```

the readonly signal reflects any changes made to the original wriable signal, but cannot be modified using `set()` or `update()` methods.

-- IMPORTANT: the readonly signals do not have any built-in mechanism that would prevent deep-mutation or their value.



#### Computed signals
computed signal are read-only signals that derive their value from other signals, you define computed signals using the `computed` function and specifying a derivation:
```ts
const count: WritableSignal<number> = signal(0);
const doubleCount: Signal<number> = computed(() => count() * 2);
```

`doubleCount` signal depends on the `count` signal.
`count` updates, Angular knows that `doubleCount` needs to update as well.

_ Computed signals are both lazily evaluated and memoized
`doubleCount`'s derivation function does not run to calculate its value until the first time you read `doubleCount`.



#### Computed signals are not writable signals
you cannot directly assign values to a computed signal. that is,
```ts
doubleCount.set(3);
```
produces a compilation error, because `doubleCount` is not a `WritableSignal`.


#### computed signal dependencies are dynamic

```ts
const showCount = signal(false);
coust count = signal(0);
coust conditionalCount = computed(() => {
    if(showCount()){
        return 'the count is ${count()}.';
    } else {
        return 'noting to see here!';
    }
})
```

### Reactive contexts
reactive context is a runtime state where angular monitors signal reads to establish adependency.
the code reading the signal is the _consumer_ and the signal being read is the _producer_.

angular automaticcally enters a reactive context when:
- `effect`, `afterRenderEffect` callback
- `computed` signal.
- `linkedSignal`.
- `resource`'s params or loader function
- rendering a component template (inbluding bindings in the host property).



#### Asserts the reactive context
`assertNotInReactiveContext` helper function to assert that code is not executing within a reactive context
```ts
import { assertNotInReactiveContext } from '@angular/core';

function subscribeToEvents() {
    assertNotInReactiveContext(subscribeToEvents);
    // safe to proceed - subscription logic here
}
```


#### reading without tracking dependencies
`computed` or `effect` without creating a dependency.
suppose that when `currentUser` changes, the value of a `counter` should be logged.
you could create an `effect` which read both signals:
```ts
effect(() => {
    console.log(`user set to ${currentUser()} and the counter is ${counter()}`);
});

// signal read from being tracked by calling its getter with `untracked`:
effect(() => {
    console.log(`user set to ${currentUser()} and the counter is ${untracked(counter())}`);
});

// `untracked` is also usefule when an effect needs to invoke some external code which shouldn't be treated as a dependency:
effect(() => {
    const user = currentUser();
    untracked(() => {
        // if the `loggingService` reads signals, they won't be counted as dependencies of this effect.
        this.loggingService.log(`user set to ${user}`);
    })
})
```

#### Reactive context and async operations
reactive context is only active for synchronous code. any signal reads that occure after an asynchronous boundary will not be tracked as dependencies.

```ts
effect(async () => {
    const currentTheme = theme(); // read before await
    const data = await fetchUserData();
    console.log(`user: ${data.name}, theme: ${currentTheme}`);
});

effect(async () => {
    // also works: signal is read before await (as function argument)
    await renderContent(docContent());
})
```


#### Advanced derivations
`computed` handles simple readonly derivations, you might find yourself needing a writable state that is dependent on other signals, for more information se the dependent state with `linkedSignal` guide.

All signal APIs are synchronous -- `signal`, `computed`, `input`, etc
deal with data that is available asynchonously. a `Resource` gives you a way to incorporate async data into your application's signal-based code and still allow you to access its data synchronously. `Async reactivity with resources`


#### Executing side effects on non-reactive APIs
synchronous or asynchronous derivations are recommended when we want to react to state changes.
`effect` or `afterRenderEffect` for those specific usecase. for more information see [side effects for non-reactive APIs guide](https://angular.dev/guide/signals/effect)


#### Reading signals in `OnPush` components
`OnPush` component's template, angular tracks the signal as a dependency of that component. when the value of that signal changes,
Angular automatically [marks] the component to ensure it gets updated the next time change detection runs, refer to the [skipping component subtress](https://angular.dev/best-practices/skipping-subtrees) guide for more information about `OnPush` components.



### Advanced topics

#### signal equality functions

```ts
import _ from 'lodash';

const data = signal(['test'], {equal: _.isEqual});

// even though this is a defferent array instance, the deep equality function will consider the values to be qual, and the signal won't trigger any updated.
data.set(['test']);
```





























[Signal Forms](https://angular.dev/tutorials/signal-forms)













[Signals Essentials](https://angular.dev/essentials/signals)
```ts
import { signal } from '@angular/core';

// create a signal with the `signal` function
const firstName = signal("Name");

// read a signal value by calling it-signals are functions.
console.log(firstName());

// change the value of this signal by calling its `set` method with a new value.
firstName.set('NewName');

// you can also use the `update` method to change the value based on the previous value.
firstName.upstae((name) => name.toUpperCase());


import { signal, computed } from '@angular/core';

const firstName = signal("Name");
const firstNameCapitalized = computed(() => firstName().toUpperCase());

console.log(firstNameCapitalized()); // NAME

firstName.set("NewName");
console.log(firstNameCapitalized()); // NEWNAME


```



















[Signals Forms](https://angular.dev/essentials/signal-forms)

```ts
// 1. create a form model with `signal()` 

interface LoginData {
    email: string;
    password: string;
}

const loginModel = signal<LoginData>({
    email: '',
    password: ''
});


// 2. pass the form model to `form()` to create a `FieldTree`
const loginFomr = form(loginModel);

// access fields directly by property name
loginForm.email;
loginForm.password;


// 3. bind HTML input with [formField] directive
<input type="email" [formField]="loginForm.email" />
<input type="password" [formField]="loginForm.password" />

-- `[formfield]` directive also syncs field state for attributes like `required`, `disabled`, `readonly` when appropriate.

// 4. read field value with `value()`

loginForm.email(): // returns FieldState with value(), valid(), touched(), etc...

// to read the field's current value, access the `value()` signal:
<p>Email: {{ loginForm.email().value() }}</p>
// render form value that updates automatically as user types

// get the current value
const currentEmail = loginForm.email().value();


// 5. update field values with `set()`
// update the value programmatically
loginForm.email().value.set('name@email.com');
// as a result, both the field value and the model signal are updted automatically:
// the model signal is also updted
console.log(loginModel().email); // 'name@email.com'


@Component({
    selector: 'xxx',
    templateUrl: 'xxx.html',
    styleUrl: 'xxx.css',
    import: [FormField],
    changeDetection: ChangeDetectionStrategy.OnPush,
})

```



### BASIC USAGE
`[formField]` directive works with all standard HTML input types.

```ts
// Text inputs -- text inputs work with various `type` attribures and textareas:
// <!-- Text and Email -->
<input type="text"  [formField]="form.name" />
<input type="email" [formField]="form.email" />


// Numbers -- number input automatically convert between strings and numbers:
<input type="number" [formField]="form.age" />


// Date and Time -- date inputs store value as `YYYY-MM-DD` string, and time inputs use `HH:mm` format:
<input type="date" [formField]="form.eventDate" />
<input type="time" [formField]="form.eventTime" />
// Date and Time - stores as ISO format strings 

// if you need to convert date strings to Date objects, you can do so by passing the field value into `Date()`:
const dateObject = new Date(form.eventDate().value());


// Multiline text -- textareas work the same way as text inputs:
<textarea [formField]="form.message" rows="4"></textarea>


// Checkboxes -- checkboxes bind to boolean value:
<label>
    <input type="checkbox" [formField]="form.agreeToTerms" />
</label>


// Radio buttons -- radio buttons work silimarly to checkboxes, as long as the radio buttons use the same `[formField]` value, Signal forms will automatically bind the same `name` attribute to all of them:
<label><input type="radio" value="free" [formField]="form.plan" /></label>
<label><input type="radio" value="premium" [formField]="form.plan"/></label>
// when a user selects a radio button, the form `formField` stores the value from that radio button's `value` attribute. from example, selecting "Premium" set `form.plan().value()` to `"premium"`.


// Select Dropdowns -- select elements work with both static and dynamic options:
// static options 
<select [formField]="form.country">
    <option value="">Select a country</option>
    <option value="us">United States</option>
    <option value="ca">Canada</option>
</select>

// dynamic options with @for
<select [formField]="form.productId">
    <option value="">Select a product</option>
    @for(product of products; track product.id){
        <option [value]="product.id">{{ product.name }}</option>
    }
</select>
```





### Validation and State
signal forms provides built-in validators that you can apply to your form fields. to add validation, pass a schema function as the second argument to `form()`:
```ts
const loginForm = form(loginModel, (schemaPath) => {
    debounce(schemaPath.email, 500);
    required(schemaPath.email);
    email(schemaPath.email);
});
```

COMMON VALIDATORS INCLUDE:
- `required()` - Ensures the field has a value
- `email()` - Validates email format
- `min()`/`max()` - Validates number ranges
- `minLength()`/`maxLength()` - Validate string or collection length
- `pattern()` - Validate against a regex pattern

you can also customize error messages by passing an options object as the second argument to the validator:
```ts
required(schemaPath.email, { message: 'Email is required'});
email(schemaPath.email, { message: 'Please enter a valid email address'});
```

each form field exposes its validation state thorugh signals. For example,
`field().valid()` to see if validation passes
`field().touched()` to see if the user has interacted with it
`field().errors()` to get the list of validation errors.



### FIELD STATE SIGNALS
ever `field()`provides these state signals:

| State | Description |
----------------------
| `valid()` | returns `true` if the field passes all validation rules |
| `touched()` | return `true` if the user has focused and blurred the field |
| `dirty()` | return `true` if the user has changed the value |
| `disabled()` | return `true` if the field is disabled |
| `readonly()` | return `true` if the field is readonly |
| `pending()` | return `true` if async validation is in progress |
| `errors()` | returnan array of valication errors with `kind` and `message` properties |







