using Azure.Messaging.ServiceBus;
using System;

namespace AzureServiceReceiver
{
    internal class Program
    {
        private static string connection_string = "Endpoint=sb://sbnamespace1986.servicebus.windows.net/;SharedAccessKeyName=Listen;SharedAccessKey=qTB79C4i3plSnjygpbvVxkT9idDciaUOo+ASbJBD6XM=;EntityPath=appqueue/$DeadLetterQueue";
        private static string queue_name = "appqueue/$DeadLetterQueue";

        static void Main(string[] args)
        {
            ServiceBusClient client = new ServiceBusClient(connection_string);
            ServiceBusReceiver receiver = client.CreateReceiver(queue_name, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock});
            var message = receiver.ReceiveMessageAsync().GetAwaiter().GetResult();
            Console.WriteLine($"Dead Letter Reason: {message.DeadLetterReason}");
            Console.WriteLine($"Dead Letter Error Description: {message.DeadLetterErrorDescription}");
            Console.WriteLine(message.Body);

            Console.ReadKey();
        }
    }
}
