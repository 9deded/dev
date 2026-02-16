```html
<button *ngIf="authService.hasRole('admin')">Admin Panel</button>
```

/// https://medium.com/@matheusluna96/authentication-and-authorization-in-angular-0697ab16e465
```ts
hasRole(role: string): boolean {
    const token = this.getToken();
    if (!token) return false;

    const payload = JSON.parse(atob(token.split('.')[1]));

    return payload.roles.includes(role);
}
```



```ts 
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler } form '@angular/common/http';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    const token = localStorage.getItem('token');

    if (token) {
        req = req.clone({ setHeaders: {Authorization: `Bearer ${token}` }});
    }

    return next.handle(req);
}


// app.model.ts
import { HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModel({
    providers: [{ provide:HTTP_INTERCEPTORS, useClass:AuthInterceptor, multi:true }]
})

export class AppModule {}
```


```ts
@Injectable({ providedIn:'root' })
export class AuthService {
    private authUrl = 'https://xxxx';

    constructor(private http:HttpClient){}

    login(credentials: { email:string, password:string}):
    Observable<any>{
        return this.http.post<{ token:string }>(`${this.authUrl}/login`, credentials).pipe(
            tab(response => localStorage.setItem('token', response.token));
        )
    }

    logout(): void {
        localStorage.removeItem('token');
    }

    getToken(): string|null {
        return localStorage.getItem('token');
    }

    isAuthenticated(): boolean {
        return !!this.getToken();
    }
}

```