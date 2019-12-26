﻿# Setup the foundation packages


## Swagger
Reference: [https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio]

1. Install-Package Swashbuckle.AspNetCore -Version 5.0.0-rc5
2. Add Swagger service
```
using Microsoft.OpenApi.Models;
...
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<TodoContext>(opt =>
        opt.UseInMemoryDatabase("TodoList"));
    services.AddControllers();

    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });
}
```
3. Configure Swagger
```
public void Configure(IApplicationBuilder app)
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```
4. Create the XML file
Open the API Project Properties, in the build tab, output path, check the XML Documentation


## SeriLog
[https://nblumhardt.com/2019/10/serilog-in-aspnetcore-3/]
[https://github.com/serilog/serilog/wiki/Configuration-Basics]


1. Add package Serilog.AspNetCore
2. Add package Serilog.Sinks.File


## EntityFramework
1. Add package Microsoft.EntityFrameworkCore.Design

## Hangfire
[https://www.hangfire.io/]
[https://docs.hangfire.io]
1. Add package HangFire

