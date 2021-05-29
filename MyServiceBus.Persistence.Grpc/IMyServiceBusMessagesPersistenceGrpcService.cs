using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MyServiceBus.Persistence.Grpc
{


    [ServiceContract(Name = "IMyServiceBusMessagesPersistenceGrpcService")]
    public interface IMyServiceBusMessagesPersistenceGrpcService
    {
        [OperationContract(Action = "GetPageCompressed")]
        IAsyncEnumerable<CompressedMessageChunkModel> GetPageCompressedAsync(GetMessagesPageGrpcRequest request);

        [OperationContract(Action = "SaveMessages")]
        ValueTask SaveMessagesAsync(IAsyncEnumerable<CompressedMessageChunkModel> request);

        [OperationContract(Action = "GetMessage")]
        ValueTask<MessageContentGrpcModel> GetMessageAsync(GetMessageGrpcRequest request);
    }



    public static class MyServiceBusMessagesPersistenceGrpcServiceExtensions
    {

        public static async IAsyncEnumerable<MessageContentGrpcModel> GetPageAsync(
            this IMyServiceBusMessagesPersistenceGrpcService grpcService, string topicId, long pageNo)
        {

            var requestModel = new GetMessagesPageGrpcRequest
            {
                TopicId = topicId,
                PageNo = pageNo
            };

            var items = await grpcService.GetPageCompressedAsync(requestModel)
                .DecompressAndMerge<IEnumerable<MessageContentGrpcModel>>();

            foreach (var itm in items)
                yield return itm;

        }

        public static ValueTask SaveMessagesAsync(this IMyServiceBusMessagesPersistenceGrpcService grpcService,
            string topicId, MessageContentGrpcModel[] messages, int packetSize)
        {
            var contract = new SaveMessagesGrpcContract
            {
                TopicId = topicId,
                Messages = messages
            };

            return grpcService.SaveMessagesAsync(contract.CompressAndSplitAsync(packetSize));
        }

    }

}