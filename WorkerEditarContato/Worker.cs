using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using ApiContatos.Application.Services.ContactService;
using ApiContatos.Domain;
using ApiContatos.Application.Dto;

namespace WorkerEditarContato
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "fila-atualizar-contato",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    // Criar escopo para resolver o IContactService
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var contactService = scope.ServiceProvider.GetRequiredService<IContactService>();

                        var newContact = JsonSerializer.Deserialize<Contact>(message);
                        contactService.UpdateContact(newContact);
                    }

                };

                channel.BasicConsume(
                    queue: "fila-atualizar-contato",
                    autoAck: true,
                    consumer: consumer);
                await Task.Delay(2000, stoppingToken);
            }
        
        }
    }
}
