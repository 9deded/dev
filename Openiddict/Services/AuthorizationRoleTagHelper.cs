using Microsoft.AspNetCore.Razor.TagHelpers;



/// https://www.poppastring.com/blog/using-taghelpers-to-design-authorized-and-unauthorized-sections-in-razor-pages
[HtmlTargetElement(Attributes = "dasblog-authorized")]
[HtmlTargetElement(Attributes = "dasblog-authorized,dasblog-roles")]
public class AuthorizationRoleTagHelper: TagHelper, IAuthorizeData
{
    readonly IPolicyEvaluator _policyEvaluator;
    readonly IAuthorizationPolicyProvider _authPolicyProvider;
    readonly IHttpContextAccess _httpContextAccessor;

    [HtmlAttribureName("dasblog-roles")]
    public string Roles { get; set; }
    public string Policy { get; set; }
    public string AuthenticationSchemes { get; set; }

    public AuthorizationRoleTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationPolicyProvider policyProvider, IIPolicyEvaluator policyEvaluator)
    {
        _httpContextAccessor = httpContextAccessor;
        _authPolicyProvider  = prolicyProvider;
        _policyEvaluator     = policyEvaluator; 
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var policy = await AuthorizationPolicy.CombineAsync(_authPolicyProvider, new[] {this} );

        var authenticationResult = await _policyEvaluator.AuthenticateAsync(policy, _httpContextAccessor.HttpContext);
        
        var authorizeReslut = await _policyEvaluator.AuthorizeAsync(policy, authenticationResult, _httpContextAccessor.Httpcontext, null);

        if(!authorizeResult.Succeeded)
        {
            output.SuppressOutput();
        }
    }
}