var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Register application services / repositories (Infrastructure implementations)
builder.Services.AddSingleton<company_backend.Application.Interfaces.ICompanyRepository, company_backend.Infrastructure.Repositories.InMemory>();
// Configure CORS to allow local frontend during development
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowLocalhostFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:59535", "http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowLocalhostFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
