using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyServiceBus.Persistence.Grpc;
using NUnit.Framework;

namespace MyServiceBus.Persistence.Tests
{
    public class CompressedMessagesTests
    {

        [Test]
        public async Task TestCompressedDecompressedMessagesAsync()
        {

            var sourceItems = new List<MessageContentGrpcModel>();

            for (var i = 0; i < 10000; i++)
            {
                sourceItems.Add(new MessageContentGrpcModel
                {
                    Data = new byte[]{1,2,3,4,6},
                    Created = DateTime.UtcNow,
                    MessageId = i
                });
            }

            var chunk = sourceItems.CompressAndSplitAsync(32);

            var result = await chunk.DecompressAndMerge<MessageContentGrpcModel[]>();
            
            Assert.AreEqual(sourceItems.Count, result.Length);

        }
        
    }
}