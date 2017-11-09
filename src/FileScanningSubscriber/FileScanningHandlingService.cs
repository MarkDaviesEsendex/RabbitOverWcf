using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Shared;
using WCFCommon;

namespace FileScanningSubscriber
{
    public class FileScanningHandlingService : IFileScanning
    {        
        public async Task HandleFileScanRequest(Guid id, string fileName, int sleepTime)
        {
            Console.WriteLine($"File Scan Command Received for file Id: {id}");
            Console.WriteLine("Sleeping for {0} second(s)", sleepTime / 1000);
            await Task.Delay(sleepTime);            

            using (var client = new ChannelFactoryWrapper<IFileDelivery>(new ChannelFactory<IFileDelivery>("*"), f => f.CreateChannel()))
            {
                await client.Invoke(async c => await c.HandleFileDeliveryRequest(id, fileName));
            }
        }
    }
}
