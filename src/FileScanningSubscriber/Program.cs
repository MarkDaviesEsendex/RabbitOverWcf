using System;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace FileScanningSubscriber
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "File Scanning Subscriber";
            CheckQueueExists();

            var serviceHost = new ServiceHost(typeof(FileScanningHandlingService));
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

            var queuePath = @".\Private$\FileScanning";
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
            // Try reducing the max concurrent calls in config to a low number such as 2, force some long running handling and then observe how service is throttled to the set number of concurrent requests
            ChannelDispatcher dispatcher = host.ChannelDispatchers[0] as ChannelDispatcher;
            if (dispatcher != null)
            {
                Console.WriteLine("The following throttling configuration is in use (this can be configured in the config file for the service) :-");
            }
        }
    }
}
