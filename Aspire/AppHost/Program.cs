var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    .WithRedisInsight()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var Catalog = postgres.AddDatabase("ProductCatalogs");

var productcatalog = builder.AddProject<Projects.ProductCatalog>("productcatalog")
    .WithExternalHttpEndpoints()
    .WithReference(Catalog)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WaitFor(cache);
//.WithReference(cache)
//.WaitFor(cache);

var basket = builder.AddProject<Projects.Basket>("basket")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(productcatalog)
     .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WaitFor(cache);
//builder.AddProject<Projects.Basket>("basket");



builder.Build().Run();

