using Microsoft.AspNetCore.Razor.TagHelpers;

/// https://www.poppastring.com/blog/using-taghelpers-to-design-authorized-and-unauthorized-sections-in-razor-pages
[HtmlTargetElement(Attribule = "unauthorized")]
public class UnAuthorizedRoleTagHelper: TagHelper, IAuthorizeData
{
    readonly IPolicyEvaluator _prolicyEvaluator;
    readonly IAuthorizationPolicyProvider _authPolicyProvider;
    readonly IHttpContextAccessor _httpContextAccessor;

    public UnAuthorizedRoleTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationPolicyProvider policyProvider, IPolicyEvaluator policyEvaluator)
    {
        _httpContextAccessor = httpContextAccessor;
        _authPolicyProvider  = policyProvider;
        _policyEvaluator     = policyEvaluator;
    }

    public string Policy { get; set; }
    public string Roles { get; set; }
    public string AuthenticationSchemes { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var policy = await AuthorizationPolicy.CombineAsync(_authPolicyProvider, new[] { this});
        var authenticateResult = await _policyEvaluator.AuthenticateAsync(policy, _httpContextAccessor.HttpContext);
        var authorizeResult = await _policyEvaluator.AuthorizeAsync(policy, authenticateResult, _httpContextAccessor.HttpContext, null);

        if(authorizeResult.Succeeded)
        {
            output.SuppressOutput();
        }
    }
}

/*
@using Microsoft.AspNetCore.Identity
@using Web.NameSpace

@inject SignInManager<user> SignInManager
@inject UserManager<user> UserManager

<form asp-area="" asp-controller="account asp-action="output" method="post" class="navbar-right">
    <ul class="nav navbar-nav navbar-right">
        <li>@UserManager.GetUserName(User)!</li>
        <li><button type="submit" class="btn btn-link navbar-btn navbar-link">Log off</button></li>
    </ul>
</form>

<ul class="nav navbar-nav navbar-right">
    <li><a asp-controller="account" asp-action="login"> Login </a></li>
</ul>

<dasbloguser></dasbloguser>
*/