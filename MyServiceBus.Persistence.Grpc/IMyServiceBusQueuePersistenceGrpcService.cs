using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MyServiceBus.Persistence.Grpc
{
    
    [ServiceContract(Name = "persistence.MyServiceBusQueuePersistenceGrpcService")]
    public interface IMyServiceBusQueuePersistenceGrpcService
    {
        [OperationContract(Action = "SaveSnapshot")] 
        ValueTask SaveSnapshotAsync(SaveQueueSnapshotGrpcRequest request);

        [OperationContract(Action = "GetSnapshot")] 
        IAsyncEnumerable<TopicAndQueuesSnapshotGrpcModel> GetSnapshotAsync();
    }
}