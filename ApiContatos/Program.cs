using ApiContatos.Application.Services.ContactService;
using ApiContatos.Infrastructure.Data.DbContexts;
using ApiContatos.Infrastructure.Repositories.ContactRepository;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, EFContactRepository>();
builder.Services.AddDbContext<ContactDbContext>(ServiceLifetime.Scoped);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
