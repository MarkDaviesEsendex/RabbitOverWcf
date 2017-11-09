using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IFileDelivery
    {
        [OperationContract(IsOneWay = true)]
        Task HandleFileDeliveryRequest(Guid id, string fileName);
    }
}
