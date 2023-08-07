using Azure.Messaging.ServiceBus;
using System;

namespace AzureServiceReceiver
{
    internal class Program
    {
        private static string connection_string = "Endpoint=sb://sbnamespace1986.servicebus.windows.net/;SharedAccessKeyName=Listen;SharedAccessKey=qTB79C4i3plSnjygpbvVxkT9idDciaUOo+ASbJBD6XM=;EntityPath=appqueue";
        private static string queue_name = "appqueue";

        static void Main(string[] args)
        {
            ServiceBusClient client = new ServiceBusClient(connection_string);
            ServiceBusReceiver receiver = client.CreateReceiver(queue_name, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock});
            var messages = receiver.ReceiveMessagesAsync(3).GetAwaiter().GetResult();
            foreach (var message in messages)
            {
                Console.WriteLine(message.SequenceNumber);
                Console.WriteLine(message.Body.ToString());
                receiver.CompleteMessageAsync(message);
            }

            Console.ReadKey();
        }
    }
}
