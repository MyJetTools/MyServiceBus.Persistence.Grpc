using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyServiceBus.MessagesCompressor;

namespace MyServiceBus.Persistence.Grpc
{
    public static class CompressUtils
    {

        public static IAsyncEnumerable<byte[]> CompressAndSplitAsync(this object contract, int packetSize)
        {
            var stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(stream, contract);

            stream.Position = 0;

            var compressedData =  new ValueTask<ReadOnlyMemory<byte>>(stream.Compress());

            return compressedData.BatchItAsync(packetSize);
        }

        public static async Task<T> DecompressAndMerge<T>(this IAsyncEnumerable<byte[]> payLoadAsync)
        {
            var compressedData = await payLoadAsync.CombineItAsync();

            var uncompressedData = compressedData.Decompress();

            return ProtoBuf.Serializer.Deserialize<T>(uncompressedData);

        }
        
    }
}