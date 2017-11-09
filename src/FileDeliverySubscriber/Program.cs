using System;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace FileDeliverySubscriber
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "File Delivery Subscriber";
            CheckQueueExists();

            var serviceHost = new ServiceHost(typeof(FileDeliveryHandlingService));
            serviceHost.Open();

            OutputThrottlingConfiguration(serviceHost);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            serviceHost.Close();
        }

        private static void CheckQueueExists()
        {
            //Check queue exists
            //Would ordinarily rely on queue existing as part of initial deployment and not include this code here but included for purposes of sample
            //Including this code necessitates a dependency on System.Messaging dll which wouldnt otherwise be required

            var queuePath = @".\Private$\FileDelivery";
            if (!MessageQueue.Exists(queuePath))
            {
                using (MessageQueue queue = MessageQueue.Create(queuePath, true))
                {
                    queue.Close();
                }
            }
        }

        private static void OutputThrottlingConfiguration(ServiceHost host)
        {
            // This is output just for demonstration purposes
            // Configure in config file
            ChannelDispatcher dispatcher = host.ChannelDispatchers[0] as ChannelDispatcher;
            if (dispatcher != null)
            {
                Console.WriteLine("The following throttling configuration is in use (this can be configured in the config file for the service) :-");
            }
        }
    }
}
