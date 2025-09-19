




# [ApplicationRef](https://angular.dev/api/core/ApplicationRef)
a reference to an Angular application running on a page.
### API



### Using Notes
`this.appRef.tick()` in angular explicitly triggers the application-wide change detection cycle.

__Explanation:__
`ApplicatonRef`:


`trick()` method:
when `this.appRef.tick()` is called, Angular iterates through all the components in the application's view tree, from the root component downwards, and performs change detection.
this means it checks for any changes in the data bound to the templates of these components and updates the DOM accordingly to reflect those changes.

__in summary: `this.appRef.tick()` forces Angular to perform a full change detection cycle across the entire application, ensuring that the UI reflects the current state of your data.__
