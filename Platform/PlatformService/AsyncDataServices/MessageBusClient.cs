using PlatformModels.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient {

    private readonly IConfiguration _conf;
    private readonly IConnection? _connection;

    public IModel? _channel { get; }

    public MessageBusClient(IConfiguration conf) {
        _conf = conf;
        var factory = new ConnectionFactory() { 
            HostName = _conf["RabbitMQHost"],
            Port = int.Parse(_conf["RabbitMQPort"]),
        };
        try {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabitMQConnectionShutdown;

            Console.WriteLine("--> Connected to Message Bus");

        }
        catch (Exception ex) {
            Console.WriteLine($"--> could not connecto to the message bus: {ex.Message}");
        }
    }

    
    private void Dispose() {
        Console.WriteLine("--> Connected to Message Bus");
        if (_channel != null && _channel.IsOpen) { 
            _channel.Close();
            _connection?.Dispose();
            
        }
    }


    private void RabitMQConnectionShutdown(object? sender, ShutdownEventArgs e) {
        Console.WriteLine($"--> Bus Connection Shutdown:");
    }

    public void PublishNewPlatform(PlatformPublishDto plat) {
        var msgObj = JsonSerializer.Serialize(plat);
        if ((_connection != null && _channel != null) && _connection.IsOpen) {
            Console.WriteLine($"--> RabbitMQ Con Open, Sending msg");
            SendMessage(msgObj);
        }
        else {
            Console.WriteLine($"--> RabbitMQ Con Closed, Could not send msg");
        }
    }

    private void SendMessage(string message) { 
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
        Console.WriteLine($"--> Message published to the bus ({body})");
    }

    
}
