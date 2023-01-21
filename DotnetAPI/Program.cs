
using DotnetAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((options) =>
{
  {
    options.AddPolicy("DevCors", (corsBuilder) =>
    {
      corsBuilder.WithOrigins("localhost:4200", "localhost:3000", "localhost:8000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
    options.AddPolicy("ProdCors", (corsBuilder) =>
    {
      corsBuilder.WithOrigins("https://myProduction.com")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
  }
});


builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseCors("DevCors");
  app.UseSwaggerUI();
}
else
{
  app.UseSwagger();
  app.UseCors("ProdCors");
  app.UseHttpsRedirection();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
