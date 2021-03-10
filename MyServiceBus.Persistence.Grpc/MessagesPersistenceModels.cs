using System;
using System.Runtime.Serialization;

namespace MyServiceBus.Persistence.Grpc
{

    [DataContract]
    public class MessageContentMetaDataItem
    {
        [DataMember(Order = 1)]
        public string Key { get; set; }

        [DataMember(Order = 2)]
        public string Value { get; set; }

    }
    

    [DataContract]
    public class MessageContentGrpcModel
    {
        [DataMember(Order = 1)]
        public long MessageId { get; set; }
        
        [DataMember(Order = 2)]
        public DateTime Created { get; set; }
        
        [DataMember(Order = 3)]
        public byte[] Data { get; set; }
        
        [DataMember(Order = 4)]
        public MessageContentMetaDataItem[] MetaData { get; set; }
        
    }
}