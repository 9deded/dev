# [HTTP Client](https://angular.dev/guide/http)
> understanding communicating with backend services using HTTP

most front-end applications need to communicate with a server over the HTTP protocol to download or upload data and access other back-end service
Angular provides a client HTTP API for Angular application, the `HttpClient` service class in `@angular/common/http`.

## HTTP client serivce function
the HTTP client service offers the following major features:
- the ability to request [typed response value](https://angular.dev/guide/http/making-requests#fetching-json-data)
- streamlined [error handling](https://angular.dev/guide/http/making-requests#handling-request-failure)
- request and response [interception](https://angular.dev/guide/http/interceptors)
- robust [testing utilities](https://angular.dev/guide/http/testing)


## [Setting up `HttpClient`](https://angular.dev/guide/http/setup)
before you can  use `HttpClient` in your app, you must configure it using [dependency injection](https://angular.dev/guide/di).

### Providing `HttpClient` through dependency injection
`HttpClient` is provided using the `provideHttpClient` helper function, which most apps include int the application `providers` in `

