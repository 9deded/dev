

[AddDbContext vs AddDbContextFactory vs AddDbContextPool](https://medium.com/@amnashahidmarm/adddbcontext-vs-adddbcontextfactory-vs-adddbcontextpool-bdc5dd797a9d)
`.AddDbContext`
it creates an instance of `DbContext` with scoped lifetime. lifetime and usually meant for single unit of work.

`.AddDbContextPool`
let the applicaitons register the pooling of `DbContext` as a service. pool can retain maxiumn of 1024 instances of DbContext. this concept is similar to the database connection pooling. an instance from pool is assigned to each request and when done, instance is reset and returned to the pool which can be resued later. this can improve performance.

`.AddDbContextFactory`
this method is recommended for **Blazor application**. it registers a factory whereas in previous methods, the context is registerd. it creates and instance of *DbContextFactory*. the lifetime of the factory is singleton by default.


```cs 
// with dependecy injection
builder.Service.AddDbContextPool<WeatherForecastContext>(option => opton.UseSqlserver(builder.ConfigurationGetConnectionString("WeatherForecastContext")));

// without dependecy injection
var options = new DbContextOptonsbuilder<PooledBloggingContext>()
                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;ConnectRetryCount=0")
                    .Options;
var factory = new PooledDbContextFactory<PooledBloggingContext>(options);

using (var context = factory.CreateDbContext())
{
    var allPosts = await context.Posts.ToListAsync();
}
```
[](https://learn.microsoft.com/en-us/ef/core/performance/advanced-performance-topics)



[Create and Drop APIs](https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created)
the `EnsureCreatedAsync` and `EnsureDeletedAsync` methods provide a lightweight alternative to `Migrations` for managing the database schema. these methods are useful in scenarios when the data is transient and can be dropped when the schema changes. for example during prototyping, in thests, or for local caches.

some provider (especially non-relational ones) dont support Migrations. for these providers, `EnsureCreatedAsync` is often the easiest way to initialize the database schema.

> WARNING `EnsureCreatedAsync` and Migrations don't work well together. if your're using Migrations, dont use `EnsureCreatedAsync` to initialize the schema.


### EnsureDeletedAsync
the `EnsureDeletedAsync` method will drop the database if it exists. if you dont have the appropriate permissions, an exception is thrown.
```cs
// drop the database if it exists
await dbContext.Database.EnsureDeletedAsync();
```

### EnsureCreatedAsync
`EnsureCreateAsync` will create the database if it doesn't exist and initialize the database schema. if any tables exist (including tables for another `DbContext` class), the schema wont be initialized.
```cs
// create the database if it doesnt exist
dbContext.Database.EnsureCreatedAsync();
```

> Asnync versions of these methods are also available.

### SQL Script
to get the SQL used by `EnsureCreatedAsync`, you can use the [GenerateCreateScript](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationaldatabasefacadeextensions.generatecreatescript) method.
```cs
var sql = dbContext.Database.GenerateCreateScript();
```

### multiple DbContext classes
EnsureCreated only works when on tables are present in the database. if needed, you write your own check to see if the schema needs to be initialized, and use the underlying `IRelationalDatabaseCreator` service to initialize the schema.
```cs
// initialize the schema for this DbContext
var databaseCreator = dbContext.GetService<IRelationalDatabaseCreator>();
    databaseCreator.CreateTables();
```
