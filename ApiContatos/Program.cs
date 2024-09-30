using ApiContatos.Application.Services.ContactService;
using ApiContatos.Infrastructure.Data.DbContexts;
using ApiContatos.Infrastructure.Repositories.ContactRepository;
using FluentValidation;
using FluentValidation.AspNetCore;
using OpenTelemetry;
using OpenTelemetry.Extensions.Hosting;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, EFContactRepository>();
builder.Services.AddDbContext<ContactDbContext>(ServiceLifetime.Scoped);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{ 
    endpoints.MapMetrics();
});
app.MapControllers();

app.Run();
