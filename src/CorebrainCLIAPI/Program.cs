using System.Reflection;
using CorebrainCLIAPI;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Dodaj usługę CORS dla frontendu (np. Vite na 5173)
builder.Services.AddCors(options => options.AddPolicy("AllowFrontend", policy =>
    policy.WithOrigins("http://localhost:5173")
          .AllowAnyMethod()
          .AllowAnyHeader()
));

// 2️⃣ Rejestracja kontrolerów i ustawień
builder.Services.AddControllers();
builder.Services.Configure<CorebrainSettings>(
    builder.Configuration.GetSection("CorebrainSettings"));

// 3️⃣ Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
  c.SwaggerDoc("v1", new OpenApiInfo {
    Title = "Corebrain CLI API",
    Version = "v1",
    Description = "ASP.NET Core Web API for interfacing with Corebrain CLI commands"
  });

  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  if (File.Exists(xmlPath)) {
    c.IncludeXmlComments(xmlPath);
  }
});

var app = builder.Build();

// 4️⃣ Middleware pipeline
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI(c =>
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Corebrain CLI API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
