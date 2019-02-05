using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using PokeDBModel;
using PokeControlController;
using System.IO;
using System.Runtime.Serialization.Json;

class RPCServer
{
    private static PokeDB pokeDB;
    private static PokeControl pokeCtrl;

    public static void Main()
    {
        pokeDB = new PokeDB(@"..\..\..\Data\pokemons.csv");
        pokeCtrl = new PokeControl();

        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "SearchType", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: "ListMultType", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: "ListAllLegendary", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: "SearchName", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: "ListHeaders", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: "SearchHeader", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: "Battle", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "SearchType", autoAck: false, consumer: consumer);
            channel.BasicConsume(queue: "ListMultType", autoAck: false, consumer: consumer);
            channel.BasicConsume(queue: "ListAllLegendary", autoAck: false, consumer: consumer);
            channel.BasicConsume(queue: "SearchName", autoAck: false, consumer: consumer);
            channel.BasicConsume(queue: "ListHeaders", autoAck: false, consumer: consumer);
            channel.BasicConsume(queue: "SearchHeader", autoAck: false, consumer: consumer);
            channel.BasicConsume(queue: "Battle", autoAck: false, consumer: consumer);
            Console.WriteLine("Awaiting requests");

            consumer.Received += (model, ea) =>
            {
                string response = null;

                var body = ea.Body;
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine("In: {0} - {1}", ea.RoutingKey, message);

                    //Create a stream to serialize the object to.  
                    MemoryStream ms = new MemoryStream();

                    Object resp = CreateResponse(ea.RoutingKey, message);

                    // Serializer the User object to the stream.  
                    DataContractJsonSerializer ser = new DataContractJsonSerializer((resp.GetType()));
                    ser.WriteObject(ms, resp);
                    byte[] json = ms.ToArray();
                    ms.Close();
                    response = Encoding.UTF8.GetString(json, 0, json.Length);

                }
                catch (Exception e)
                {
                    response = "Error: " + e.Message;
                }
                finally
                {
                    Console.WriteLine("Out: {0}", response);
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Used to route the message to the correct method
    /// </summary>
    /// <param name="routingKey">The route</param>
    /// <param name="message">The message recieved</param>
    /// <returns></returns>
    private static Object CreateResponse(string routingKey, string message)
    {
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(message));
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(string[]));
        string[] args = ser.ReadObject(ms) as string[];
        ms.Close();

        switch (routingKey)
        {
            case "SearchType":
                {
                    return pokeDB.SearchType(args[0]);
                }
            case "ListMultType":
                {
                    return pokeDB.ListMultiType();
                }
            case "ListAllLegendary":
                {
                    return pokeDB.ListAllLegendary();
                }
            case "SearchName":
                {
                    return pokeDB.SearchName(args[0]);
                }
            case "ListHeaders":
                {
                    return pokeDB.Headers;
                }
            case "SearchHeader":
                {
                    return pokeDB.SearcHeader(args[0], Int32.Parse(args[1]));
                }
            case "Battle":
                {
                    return pokeCtrl.Battle(args[0], args[1], pokeDB);
                }

            default:
                {
                    throw new InvalidOperationException("Route not found");
                }

        }

    }


}
