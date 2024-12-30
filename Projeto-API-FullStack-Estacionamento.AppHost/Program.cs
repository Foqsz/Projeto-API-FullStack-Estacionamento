using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder
    .AddProject<Projects.Projeto_API_BackEnd_Estacionamento>("projeto-api-backend-estacionamento")
    .WithReference(cache);

builder.AddProject<Projects.Estacionamento_FrontEnd>("estacionamento-frontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
