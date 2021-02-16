using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyServiceBus.Persistence.Grpc
{
    public static class BatchingUtils
    {
        public static async IAsyncEnumerable<byte[]> BatchItAsync(this ValueTask<ReadOnlyMemory<byte>> srcAsync, int batchSize)
        {

            var src = await srcAsync;
            
            var remains = src.Length;
            var position = 0;

            while (remains>0)
            {
                var chunkSize = remains > batchSize ? batchSize : remains;
                yield return  src.Slice(position, chunkSize).ToArray();

                position += chunkSize;
                remains -= chunkSize;
            }
        }


        public static async ValueTask<ReadOnlyMemory<byte>> CombineItAsync(this IAsyncEnumerable<byte[]> payLoadAsync)
        {
            var result = new MemoryStream();

            await foreach (var payLoad in payLoadAsync)
            {
                result.Write(payLoad);
            }

            var buffer = result.GetBuffer();
            return new ReadOnlyMemory<byte>(buffer, 0, (int)result.Length);
        }
        

    }
}