# (Grid)[https://material.angular.dev/components/grid-list/overview]

`mat-grid-list` is a two-dimensional list view that arranges cells into grid-based layout. (see Material Design spec)[https://material.io/design/components/image-lists.html]

### setting 
an `mant-grid-list` must specify a `cols` attribute which sets the number of columns in the grid.
The number of rows will be automatically determined based on the number of columns and the number of items.

### Setting the row height
the height of the rows in a grid list can be set via the `rowHeight` attribute. row height for the list can be calculated in three ways:
1. __Fixed height__: the height can be in `px`, `em`, or `rem`. if no units are specified, `px` units are asumed (e.g. 100px, 4em, 250).
2. __Ratio__: this ratio is column-width:row-height, and must be passed in with a colon, not a decimal (e.g. 4:3).
3. __Fix__: setting `rowHeight` to `fit` this mode automatically divides the available height by the number of rows.
please note the height of the grid-list-or its container must be set.
