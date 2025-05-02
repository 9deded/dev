using Microsoft.AspNetCore.Razor.TagHelpers;



[HtmlTargetElement("user-info")]
public class UserInfoTagHelper : TagHelper
{
    public string UserName { get; set; }
    public string UserRole { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.AppendHtml($"<p>User: {UserName}</p>");
        output.Content.AppendHtml($"<p>Role: {UserRole}</p>");
    }
}

// <user-info username="JohnDoe" userrole="Admin"></user-info>