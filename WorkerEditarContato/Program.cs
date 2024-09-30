using ApiContatos.Application.Services.ContactService;
using ApiContatos.Infrastructure.Data.DbContexts;
using ApiContatos.Infrastructure.Repositories.ContactRepository;
using OpenTelemetry.Metrics;
using OpenTelemetry;
using Prometheus;
using WorkerEditarContato;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, EFContactRepository>();
builder.Services.AddDbContext<ContactDbContext>(ServiceLifetime.Scoped);

var host = builder.Build();
host.Run();
