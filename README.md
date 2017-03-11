# Reshelf
Reshelf is a library for simplifying the task of running dotnet core services on Linux.

Reshelf will listen to SIGINT and SIGTERM signals and try to shut the service down gracefully.

The syntax is heavily influenced by the excellent library Topshelf.

## Usage
```csharp
ServiceFactory.Start(config =>
{
    config.Service<Service>(service =>
    {
        service.ConstructUsing(() => new Service());
        service.OnStart(async p => await p.Start());
        service.OnStop(async p => await p.Stop());
    });
});
```
## Use Log4Net integration
```csharp
ServiceFactory.Start(config =>
{
    config.UseLog4Net();

    config.Service<Service>(service =>
    {
        service.ConstructUsing(() => new Service());
        service.OnStart(async p => await p.Start());
        service.OnStop(async p => await p.Stop());
    });
});
```

## Use Autofac to create service instance
```csharp
ServiceFactory.Start(config =>
{
    config.Service<Service>(service =>
    {
        service.UseAutofac(container);
        service.OnStart(async p => await p.Start());
        service.OnStop(async p => await p.Stop());
    });
});
```

## Dependency
.NET Standard Library 1.5
