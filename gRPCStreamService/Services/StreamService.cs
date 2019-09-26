using System;
using System.Threading.Tasks;
using Grpc.Core;
using gRPCStreamService.Stream;

namespace gRPCStreamService.Services
{
    public class StreamService : Stream.Stream.StreamBase
    {
        public async override Task GetStream(StreamRequest request, IServerStreamWriter<StreamResponse> responseStream, ServerCallContext context)
        {
            var random = new Random();
            
            for (int i = 0; i < request.Amount; i++)
            {
                await Task.Delay(1500);
                var streamResponse = new StreamResponse
                {
                    Value = random.Next()
                };
                await responseStream.WriteAsync(streamResponse);
            }            
        }
    }
}
