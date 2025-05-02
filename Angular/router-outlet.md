The <router-outlet> is a directive from the Angular router library that serves as a placeholder in a template. Angular dynamically renders the component associated with the current URL route within this placeholder. It is essential for creating single-page applications (SPAs) where different views are displayed without reloading the entire page. 
Usage
To use <router-outlet>, it must be added to a component's template, typically in the root AppComponent or within a layout component. When the application navigates to a new route, the router determines the corresponding component and renders it inside the <router-outlet>.

```html
<router-outlet> </router-outlet>
```

### Named Router Outlets
Angular supports multiple router outlets in a single template, each identified by a unique name. Named outlets enable the display of multiple components simultaneously, useful for advanced routing scenarios like displaying a sidebar or modal alongside the main content.
~~~html
<router-outlet name="sideber"> </router-outlet>
<router-outlet name="main"> </router-outlet>
~~~

To target a specific named outlet, configure routes with the outlet property.
~~~js
const routes: Routes = [
  { path: 'page', component: PageComponent, outlet: 'main' },
  { path: 'sidebar', component: SidebarComponent, outlet: 'sidebar' },
];
~~~


### Events
The RouterOutlet emits events related to component activation and deactivation.
- activate: Emitted when a new component is instantiated in the outlet.
- deactivate: Emitted when a component is destroyed.
- attached: Emitted when the RouteReuseStrategy instructs the outlet to reattach the subtree. 
- detached: Emitted when the RouteReuseStrategy instructs the outlet to detach the subtree. 


### Example
A basic setup involves adding <router-outlet> to app.component.html and configuring routes using RouterModule.forRoot() in the AppModule.

~~~ts
// app-routing.module.ts
const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
});

export class AppRoutingModule {}
~~~