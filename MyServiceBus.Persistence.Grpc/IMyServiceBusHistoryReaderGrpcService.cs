using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MyServiceBus.Persistence.Grpc
{

    [DataContract]
    public class GetHistoryByDateGrpcRequest
    {
        [DataMember(Order = 1)]
        public string TopicId { get; set; }
        [DataMember(Order = 2)]
        public DateTime FromDateTime { get; set; }
    }
    
    [ServiceContract(Name = "persistence.MyServiceBusHistoryReaderGrpcService")]
    public interface IMyServiceBusHistoryReaderGrpcService
    {
        [OperationContract(Action = "GetByDate")]
        IAsyncEnumerable<MessageContentGrpcModel> GetByDateAsync(GetHistoryByDateGrpcRequest request);
    }
}