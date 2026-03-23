



```cs <3.0
public class HomeController: Controller
{
    private readonly IWebHostEnvironment _env;

    public Homecontroller(IWebHostEnvironment env)
    {
        _env = env;
    }
}
```
[](https://mariusschulz.com/blog/getting-the-web-root-path-and-the-content-root-path-in-asp-net-core)