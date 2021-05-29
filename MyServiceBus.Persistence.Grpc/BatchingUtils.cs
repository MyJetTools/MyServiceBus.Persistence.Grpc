using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyServiceBus.Persistence.Grpc
{
    public static class BatchingUtils
    {
        public static async IAsyncEnumerable<CompressedMessageChunkModel> BatchItAsync(this ValueTask<ReadOnlyMemory<byte>> srcAsync, int batchSize)
        {

            var src = await srcAsync;
            
            var remains = src.Length;
            var position = 0;

            while (remains>0)
            {
                var chunkSize = remains > batchSize ? batchSize : remains;
                yield return new CompressedMessageChunkModel
                {
                    Chunk = src.Slice(position, chunkSize).ToArray()
                };

                position += chunkSize;
                remains -= chunkSize;
            }
        }


        public static async ValueTask<ReadOnlyMemory<byte>> CombineItAsync(this IAsyncEnumerable<CompressedMessageChunkModel> payLoadAsync)
        {
            var result = new MemoryStream();

            await foreach (var payLoad in payLoadAsync)
            {
                result.Write(payLoad.Chunk);
            }

            var buffer = result.GetBuffer();
            return new ReadOnlyMemory<byte>(buffer, 0, (int)result.Length);
        }
        

    }
}