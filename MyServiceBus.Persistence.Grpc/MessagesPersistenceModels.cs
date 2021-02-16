using System;
using System.Runtime.Serialization;

namespace MyServiceBus.Persistence.Grpc
{

    [DataContract]
    public class MessageContentGrpcModel
    {
        [DataMember(Order = 1)]
        public long MessageId { get; set; }
        
        [DataMember(Order = 2)]
        public DateTime Created { get; set; }
        
        [DataMember(Order = 3)]
        public byte[] Data { get; set; }
        
    }
}