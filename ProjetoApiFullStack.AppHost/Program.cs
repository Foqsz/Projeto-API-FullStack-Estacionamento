var builder = DistributedApplication.CreateBuilder(args);

var products = builder.AddProject<Projects.>("products");

builder.AddProject<Projects.Store>("store")
    .WithExternalHttpEndpoints()
    .WithReference(products);

builder.Build().Run();