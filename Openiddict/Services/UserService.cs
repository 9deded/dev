using Microsoft.AspNetCore.Http;

namespace x;

/// https://www.telerik.com/blogs/how-to-get-httpcontext-asp-net-core
public class UserService: IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetLoginUserName()
    {
        return _httpContextAccessor.HttpContext.User.Identity.Name;
    }
}