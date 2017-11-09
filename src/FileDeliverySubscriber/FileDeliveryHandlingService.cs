using System;
using System.Threading.Tasks;
using Shared;

namespace FileDeliverySubscriber
{
    public class FileDeliveryHandlingService : IFileDelivery
    {
        public async Task HandleFileDeliveryRequest(Guid id, string fileName)
        {
            Console.WriteLine($"File Delivery Command Received for file Id: {id}");            
        }
    }
}
