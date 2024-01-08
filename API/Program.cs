using API;
using API.Configs;
using Application;
using Infra;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.AddConfigs(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfigs();

builder.Services.AddApplication();

builder.Services.AddInfra(builder.Configuration);


builder.Services.AddResponseCompression(opt =>
{
    opt.Providers.Add<GzipCompressionProvider>();
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/json"]);
});


builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors("CorsPolicy");

app.ConfigureExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
