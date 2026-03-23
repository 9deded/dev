# Razor Pages
[Architecture and concepts](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/)


`_ViewStart.cshtml` and `_ViewImports.cshtml`

```cs 
/// Pages/_ViewStart.cshtml
@{
    Layout = "_Layout";
}
```

```cs
/// Pages/_ViewImports.cshtml
@namespace RazorPagesContacts.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```