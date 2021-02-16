using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyServiceBus.Persistence.Grpc
{
    
    [DataContract]
    public class GetMessagesPageGrpcRequest
    {
        [DataMember(Order = 1)]
        public string TopicId { get; set; }
        
        [DataMember(Order = 2)]
        public long PageNo { get; set; }
    }


    [DataContract]
    public class SaveMessagesGrpcContract
    {
        [DataMember(Order = 1)]
        public string TopicId { get; set; }
        
        [DataMember(Order = 2)]
        public MessageContentGrpcModel[] Messages { get; set; }
    }
    
    
    [DataContract]
    public class GetMessageGrpcRequest
    {
        [DataMember(Order = 1)]
        public string TopicId { get; set; }
        
        [DataMember(Order = 2)]
        public long MessageId { get; set; }
    }
}