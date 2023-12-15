using API;
using API.Configs;
using API.Services;
using Application;
using Application.Common.Interfaces;
using Application.Common.Models.FileService;
using Infra;
using Microsoft.AspNetCore.ResponseCompression;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigs(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfigs();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddApplication();

builder.Services.AddInfra(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("DevelopPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


builder.Services.AddCors(o => o.AddPolicy("ProductionPolicy", builder =>
{
    builder.WithOrigins("https://app.youin.digital")
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

builder.Services.AddResponseCompression(opt =>
{
    opt.Providers.Add<GzipCompressionProvider>();
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});




var app = builder.Build();

var policy = "ProductionPolicy";

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });

    policy = "DevelopPolicy";
//}

// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(policy);

app.ConfigureExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
