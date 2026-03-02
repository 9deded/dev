

[Signals](https://angular.dev/tutorials/signal-forms)


[Signal Forms](https://angular.dev/tutorials/signal-forms)
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

/// https://angular.dev/essentials/signals
```


[Signals Forms Essentials](https://angular.dev/essentials/signal-forms)

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







