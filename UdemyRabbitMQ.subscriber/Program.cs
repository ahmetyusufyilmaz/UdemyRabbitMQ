using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace UdemyRabbitMQ.subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://bzzotmns:u_1FPPhjTV1-emUaiViy1AmDr_gdSxoP@whale.rmq.cloudamqp.com/bzzotmns");

            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // channel.QueueDeclare("hello-queue", true, false, false);

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("hello-queue", false, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj: " + message);

                channel.BasicAck(e.DeliveryTag, false);
            };
        }

      
    }
}
