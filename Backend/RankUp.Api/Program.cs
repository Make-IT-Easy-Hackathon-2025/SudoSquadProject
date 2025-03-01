using RankUpp.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddBackendServices();

builder.Services.AddConfigurations(builder.Configuration);

builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.ConfigureJwtAuthentication(builder.Configuration);

builder.Services.AddBearerSecurityToSwagger();


builder.Services.ConfigureAutoMapper();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "customPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseCors("customPolicy");

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
