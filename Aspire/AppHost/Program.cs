var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    .WithRedisInsight()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var Catalog = postgres.AddDatabase("ProductCatalogs");

var productcatalog = builder.AddProject<Projects.ProductCatalog>("productcatalog")
    .WithExternalHttpEndpoints()
    .WithReference(Catalog)
    .WaitFor(Catalog);
    //.WithReference(cache)
    //.WaitFor(cache);

var basket = builder.AddProject<Projects.Basket>("basket")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(productcatalog)
    .WaitFor(cache);
//builder.AddProject<Projects.Basket>("basket");

builder.Build().Run();

