using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Shared;
using WCFCommon;

namespace Publisher
{
    class Program
    {
        private static async Task Main()
        {
            Console.Title = "Publisher Example";

            Console.WriteLine("Press '1' to publish the file scan");
            Console.WriteLine("Press '2' to simulate a long running file scan");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ConsoleKey.D1)
                {
                    await PublishFileScan(1000);
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    await PublishFileScan(30000);
                }
                else
                {
                    return;
                }
            }
        }

        private static async Task PublishFileScan(int sleepTime)
        {
            Guid fileId = Guid.NewGuid();
            string fileName = "File.jpg";

            using (var client = new ChannelFactoryWrapper<IFileScanning>(new ChannelFactory<IFileScanning>("*"), f => f.CreateChannel()))
            {
                await client.Invoke(async c => await c.HandleFileScanRequest(fileId, fileName, sleepTime));
            }
            
        }
    }
}
