using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IFileScanning
    {
        [OperationContract(IsOneWay = true)]
        Task HandleFileScanRequest(Guid id, string fileName, int sleepTime);        
    }
}
