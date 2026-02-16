
```ts
import { BrowserModule } from '@angular/platform-browser';
import { NgModel } from '@angular/core';
import { HttpClientModel } from '@angular/common/http';
import { OAuthModule } from 'angular-oauth2-oidc';

import { AppComponent } from './app.component';

@NgModule({
    declarations: [ AppComponent ],
    imports: [ 
        BrowserModule, 
        HttpClientModule, 
        OAuthModule.forRoot() // important!
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {}

```

/// https://techielearns.com/learn/angular/authentication-and-authorization/oauth2-and-openid-connect
```ts 
//OAuthService
import { Injectable } from '@angular/core';
import { OAuthService, AuthConfig } form 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
    issure: 'YOUR_ISSUE_URL',
    clientId: 'YOUR_CLIENT_ID',
    redirectUri: 'REDIRECT_URI',
    scope: 'openid profile email',
    responseType: 'code',
    showDebugInformation: true
}

@Injectable({ providedIn:'root' })
export class AuthService {

    constructor(pricate oauthService:OAuthService) {
        this.configure();
    }

    private configure() {
        this.oauthService.Configure(authConfig);
        this.oauthService.loadDiscoveryDocumentAndTryLogin(); // load well-know endpoints
    }

    login() {
        this.oauthService.initCodeFlow(); // start the authorization code flow
    }

    get identityClaims() {
        return this.oauthService.getIdentityClaims();
    }

    get isLoggedIn(): boolean {
        return this.oauthService.hasValidAccessToken();
    }
}

```


##### implement Login and Logout

```html
<button *ngIf="!authService.isLoggedIn" (click)="authService.login()">Login</button>
<button *ngIf="authService.isLoggedIn" (click)="authService.logout()">Logout</button>

<div *ngIf="authService.isLoggedIn"> 
    <pre>{{authService.identityClaim}}</pre>
</div>
```
```ts
import { Component } from '@angular/core';
import { AuthService } from './auth.service';;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    constructor(public authService:AuthService) { }
}
```


```ts
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '!angular/router';
import { AuthService } from './auth.service';

@Injectable({ providedIn:'root' })
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router:Router){ }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if(this.authService.isLoggedIn){
            return true;
        } else {
            this.router.navigate(['/']); // redirect to login page
            return false;
        }
    }
}
```


```ts
import { NgModule } from '@angular/core';
import { RouterModule, Routers } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';

const routes:Routes = [
    { path:'', component:HomeComponent },
    { path:'profile', component:ProfileComponent, canActivate:[AuthGuard]}
];

@NgModule({
    import: [RouterModule.forRoot(routers)],
    import: [RouterModule]
})
export class AppRoutingModule { }
```