
using FilesQueueConsumer.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatsdClient;
using System.Text;

Metrics.Configure(new MetricsConfig
{
    StatsdServerName = "127.0.0.1",
    Prefix = "myApp"
});


var factory = new ConnectionFactory() { HostName = "host.docker.internal" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "files",
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    var listFiles = new List<FileDTO>();
    var filesCount = new Dictionary<string, int>();
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var obj = JsonConvert.DeserializeObject<FileDTO>(message);
        obj.ValidarInformacao();
        
        if (filesCount.ContainsKey(obj.NomeAplicacao))
        {
            filesCount[obj.NomeAplicacao] += 1;
        }
        else
        {
            filesCount.TryAdd(obj.NomeAplicacao, 1);    
        }
        
        var counter = filesCount.GetValueOrDefault(obj.NomeAplicacao);
        Metrics.GaugeAbsoluteValue("aplicattions."+obj.NomeAplicacao , counter);

        Console.WriteLine(" count = {0} for file {1} {2}", counter,obj.NomeAplicacao,DateTime.Now);
    };
    channel.BasicConsume(queue: "files",
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}
