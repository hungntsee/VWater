using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ {
    public class Send
    {
          public void SendMessage(string message)
            {
               var factory = new ConnectionFactory()
               {
                    HostName = "194.233.89.91",
                    Port = 31740,
                    UserName = "user",
                    Password = "K6JNZEAu3itSIFTZ"
               };
               var connection = factory.CreateConnection();
               var channel = connection.CreateModel();

               var body = Encoding.UTF8.GetBytes(message);

               channel.QueueDeclare("shipper-app", durable: true, exclusive: false, autoDelete: false, arguments: null);
               channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "shipper-app",
                                     basicProperties: null,
                                     body: body);
          }

     }
}
