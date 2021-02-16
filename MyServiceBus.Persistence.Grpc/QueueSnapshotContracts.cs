using System.Runtime.Serialization;

namespace MyServiceBus.Persistence.Grpc
{

    [DataContract]
    public class SaveQueueSnapshotGrpcRequest
    {
        [DataMember(Order = 1)]
        public TopicAndQueuesSnapshotGrpcModel[] QueueSnapshot { get; set; }
    }
}