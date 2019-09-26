using Grpc.Net.Client;
using gRPCStreamService.Stream;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static gRPCStreamService.Stream.Stream;

namespace gRPCStreamClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Inicializando cliente...");
            var client = GetClient();
            var request = new StreamRequest
            {
                Amount = 10
            };

            Console.WriteLine("Iniciando stream...");
            var stream = client.GetStream(request);

            while (await stream.ResponseStream.MoveNext())
            {
                var current = stream.ResponseStream.Current;

                Console.WriteLine(current.Value);
            }

            Console.WriteLine("Finalizando stream...");
            Console.WriteLine(stream.GetStatus());
        }

        static StreamClient GetClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001");
            return GrpcClient.Create<StreamClient>(httpClient);
        }
    }
}
