




```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ItemSettings>(ItemSettings.Month, builder.Configuration.GetSection("Item:Month"));
builder.Services.Configure<ItemSettings>(ItemSettings.Year, builder.Configuration.GetSection("Item:Year"));

```
```cs
public class ItemSettings 
{
    public const string Month = "Month";
    public const string Year  = "Year";

    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
}
```
```json
{
    "Item": {
        "Month": { "Name": "Green Widget", "Model": "GW46" },
        "Year": { "Name": "Orange Gedget", "Model": "OG35" }
    }
}
```
```cs
public class TesmModel: PageModel 
{
    private readonly ItemSetting _monthItem;
    private readonly ItemSetting _yearItem;

    public TestModel(IOptionsSnapshot<ItemSettings> optionsAccessor)
    {
        _monthItem = optionsAccessor.Get(ItemSettings.Month);
        _yearItem  = optionsAccessor.Get(ItemSettings.Year);
    }
}
```


```cs
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<MyConfigOptions>(builder.Configuration.GetSection(MyConfigOptions.MyConfig));
builder.Services.AddSingleton<IValidateOptons<MyConfigOptions>, MyConfigValidation>();

var app = builder.Build();
```






```cs

builder.Services.AddOptions<MyConfigOptions>()
                .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig));

builder.Services.PostConfigureAll<MyConfigOptions>(options => {
    options.Key = "post_configured_key_value";
});
```

