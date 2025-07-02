
















[EF core 9.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew)
```cs
modelBuilder
    .Entyty<UserSection>()
    .HasPartitionKey(e => new { e.TenantId, e.UserId, e.SessionId });

```



### [Tracking vs. No-Tracking Queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking)








### [Data Seeding](https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding)