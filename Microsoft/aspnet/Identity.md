







[](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration)

```cs
public async Task<IActionResult> OnPostAsync(string returnUrl = null){
    returnUrl ??= Url.Content("~/");

    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

    if(ModelState.IsValid)
    {
        // this doesn't count login faillures towards account lockout to enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

        if(result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        if(result.RequiresToFactor)
        {
            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        }
        if(result.IsLockedOut)
        {
            _logger.LogWarning("user account locked out.");
            return RedirectToPage("./Lockout");
        }
        else{
            ModelState.AddModelError(string,)
        }

    }
}

```





















```cs
Microsoft.AspNetCore.Identity.IdentityBuilder
```

| Name | Description |
|------|-------------|
| AddIdentity<TUser,TRole>(IServiceCollection) | Adds the default identity system configuration for the specified User and Role types. |
| AddIdentity<Tuser,TRole>(IServiceCollection, Action<IdentityOptions>) | Add and configures the identity system for the specified User and Role |



```cs


```