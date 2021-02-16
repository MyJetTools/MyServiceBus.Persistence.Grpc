using System.Runtime.Serialization;

namespace MyServiceBus.Persistence.Grpc
{

    [DataContract]
    public class QueueIndexRangeGrpcModel
    {
        [DataMember(Order = 1)]
        public long FromId { get; set; }
        
        [DataMember(Order = 2)]
        public long ToId { get; set; }
    }

    [DataContract]
    public class QueueSnapshotGrpcModel
    {
        [DataMember(Order = 1)]
        public string QueueId { get; set; }
        
        [DataMember(Order = 2)]
        public QueueIndexRangeGrpcModel[] Ranges { get; set; }
    }
    
    [DataContract]
    public class TopicAndQueuesSnapshotGrpcModel
    {
        [DataMember(Order = 1)]
        public string TopicId { get; set; }
        
        [DataMember(Order = 2)]
        public long MessageId { get; set; }
        
        [DataMember(Order = 3)]
        public QueueSnapshotGrpcModel[] QueueSnapshots { get; set; }
    }
    
}