 
using Contact.Application;
using Contact.Infrastructure;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Host.ConfigureSerilog(builder.Services, builder.Configuration);
//builder.Services.ConfigureAuth(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://10.34.8.90/");
                      });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseForwardedHeaders();
//app.UseMiddleware<RequestCorrelationIdMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
//app.UseMiddleware<RequestLogContextMiddleware>();

app.MapControllers();

app.Run();
