# [Route](https://angular.dev/guide/routing)
| routing helps you change what the user sees in a single-page app.

Angular Router (`@angular/router`) is the official library for managing navigation in Angular application and a core part of the framework. it is included by default in all projects created by Angular CLI.




## [Define routes](https://angular.dev/guide/routing/define-routes)
| routes serve as the fundamental building blocks for navigation within an Angular app.

routers
| in angular, a route is an object that defines which component should render for a specific url path or pattern, as well as additional configuration options about what happens when a user navigates to that url.

example of a route:
```ts
import { AdminPage } from './app-admin/app-admin.component';

const adminPage = { part: 'admin', component: AdminPage }
```
| from this route, when a user visits the `/admin` path, the app will display the `AdminPage` component.


#### managing routes in your application
| most projects define routes in a separate file that contains `routes` in the filename.

a collection of routes looks like this:
```ts
import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page.component';
import { AdminPage } from './about-page/admin-page.component';

export const routers:Routers = [
    { path: '', component: HomePage },
    { path: 'admin', component: AdminPage }
];
```
|trip: if you generated a project with angualar cli, your routes are defined in `/src/app/app.routers.ts`.


#### Adding the router to your application
when bootstrapping an angular application without the angular cli, you can pass a configuration object that includes a `providers` array.

inside of the `providers` array, you can add the angular router to your application by adding a `provideRouter` function call with your routers.

```ts
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routers } from './app.routes';

export const appConfig: ApplicationConfig = {
    providers: [
        provideRouter(routers),
        // ...
    ]
};
```

### Route URL Paths

#### static URL paths
static url paths refer to routes with predefined paths that don't change based on dynamic parameters.
these are routers that match a `path` string exactly and have a fixed outcome.

examples of this include:
- "/admin"
- "/blog"
- "/settings/account"


#### Define url paths with route parameters
parameterized URLs allow you to define dynamic paths that allow multiple URLs to the same component while dynamically displaying data based on parameters in the URL.

you can define this type of pattern by adding parameters to your toute's `path` string and prefixing each parameter with the colon (`:`) charactor.

| IMPORTANT: parameters are distinct from information in the url's [query string](https://en.wikipedia.org/wiki/Query_string). learn more about [query parameters in angular in this guide](https://angular.dev/guide/routing/read-route-state#query-parameters).

the following example displays a user profile component based on the user id passed in throught the url.

```ts
import { Routes } from '@angular/router';
import { UserProfile } from './user-profile/user-profile';

const routes: Routes = [
    { path: 'user/:id', component: UserProfile }
];
```

in this example, URLs such as `/user/leeroy` and `/user/jenkins` render the `UserProfile` component.
this component can then read the `id` parameter and use it to perform additional work, such as fetching data. see [reading route state guide](https://angular.dev/guide/routing/read-route-state) for details on reading route parameters.

valid route parameter names must start with a letter (a-z, A-Z) and can only contain:
- Letter (a-z, A-Z)
- Numbers (0-9)
- Underscore (_)
- Hyphen (-)

you can also define paths with multiple parameters:

```ts
import { Routes } from '@angular/router';
import { UserProfile } from './user-profile/component';
import { SocialMediaFeed } from './user-profile/social-media-feed.component';

const routes: Routes = [
    { path: 'user/:id/:social-media', component: SocialMediaFeed },
    { path: 'user/:id/' component: UserProfile }
]
```
with this new path, users can visit `/user/leeroy/youtube` and `/user/leeroy/bluesky` and see respective social media feeds based on the parameter for the user leeroy.

see [reading route state](https://angular.dev/guide/routing/read-route-state) for details on reading route parameters.


### Wildcards
when you need to catch all routes for a specific path, the solution is a wildcard route which is defined with the double asterick(`**`).

a common example is defining a Page Not found component.