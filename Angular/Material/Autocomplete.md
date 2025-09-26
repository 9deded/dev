# [Autocomplete](https://material.angular.dev/components/autocomplete/overview)
the autocomplete is a normal text input enhanced by a panel of suggested options.

### Simple Autocomplete
start by creating the autocomplete panel and the options displayed inside it.
each option should be defined by a `mat-option` tag.
set each option's value property to whatever you'd like the value of the text input to be when that option is selected.
```html
<mat-autocomplete #auto="matAutocomplete">
    @for (option of options; track option){
        <mat-option [value]="option">{{option}}</mat-option>
    }
</mat-autocomplete>
```
next, create the input and set the `matAutocomplete` input to refer to the template reference we assigned to the autocomplete.
let's assume you're using the `formControl` directive from `ReactiveFormsModule` to track the value of the input.

> Note: it is possible to use template-driven forms instead, if you prefer. we use reactive forms in this example because it makes subscribing to changes in the input's value easy. for this example, be sure to import `ReactiveFormsModule` from `@angular/forms` into your `NgModule`. if you are unfamiliar with using reactive forms, you can read more about the subject in the [Angular documentation](https://angular.dev/guide/forms/reactive-forms).

now we'll need to link the text input to its panel. we can do this by exporting the autocomplete panel instance into a local template variable (here we called it "auto"), and binding that variable to the input's `matAutocomplete` property.
```html
<input type="text"
        placeholder="Pick one"
        aria-label="Number"
        matInput
        [formControl]="myControl"
        [matAutocomplete]="auto" >
```

#### Adding a custom filter
at this point, the autocomplete panel should be toggleable on focus and options should be selectable.
but if we want our options to filter when we type, we need to add a custom filter.

you can filter the options in any way you like based on the text input*. here we will perform a simple string test on the option value to see if it matches the input value, starting from the options's first letter. we already have access to the built-in `valueChanges` Observable on the `FormControl`, so we can simply map the text input's values to the suggested options by passing them through this filter. the resulting Observable, `filteredOptions`, can be added to the template in place of the `options` property using the `async` pipe.

below we are also priming our value change stream with an empty string so that the options are filtered by the value on init (before there are any value changes).

*For optimal accessibility, you may want to consider adding text guidance on the page io explain filter criteria
this is especially helpful for screenreader users if you're using a non-standard filter that doesn't limit matches to the beginning of the string.










### API
API reference for Angular Material autocomplete
`import { MatAutocompleteModule } from '@angular/material/autocomplete';`

#### Components
__MatAutocomplete__
Autocomplete component.
select: `mat-autocomplete`
exported as: `matAutocomplete`

##### Properties




### CODE

```ts

    ngOnInit(){
        this.filteredOptions = this.control.valueChanges.pipe(
            startWith(''),
            map(value => this.filter1(value || '')),
        );
    }

    private filter1(value:string): string[]{
        const filterValue = value.toLowerCase();

        return this.options.filter(option => option.toLowerCase().includes(filterValue));
    }
```
