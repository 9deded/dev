












### sass scss
- & (Ampersand): Represents the full parent selector.
- #{} (String Interpolation): Allows you to embed Sass expressions (including variables, functions, and the parent selector) within a string.
- #{&}: This combines the two, taking the value of the parent selector (&) and inserting it as a string into a larger string. This is particularly useful when you're dynamically generating class names or selectors.

```css
.message-error {
    background-color: red;

    @at-root p#{&} {
        background-color: yello;
    }
}
```
parent after child connect. 

```css
.block {
  &__element { // Compiles to .block__element
    color: black;
  }

  // Using #{&} for dynamic class generation
  @each $modifier in (primary, secondary) {
    #{&}--#{$modifier} { // Compiles to .block--primary, .block--secondary
      border: 1px solid $modifier;
    }
  }
}
```


[The Sass Ampersand](https://css-tricks.com/the-sass-ampersand/)