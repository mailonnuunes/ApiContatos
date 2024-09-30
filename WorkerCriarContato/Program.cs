using ApiContatos.Application.Services.ContactService;
using ApiContatos.Infrastructure.Data.DbContexts;
using ApiContatos.Infrastructure.Repositories.ContactRepository;
using OpenTelemetry.Metrics;
using OpenTelemetry;
using Prometheus;
using WorkerCriarContato;

var builder = Host.CreateApplicationBuilder(args);
// Registrar ContactService e ContactRepository como Scoped
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, EFContactRepository>();
builder.Services.AddDbContext<ContactDbContext>(ServiceLifetime.Scoped);
builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();
