using Microsoft.Extensions.Logging;
using Grpc.Core;
using System.Threading.Tasks;
using System;
using GrpcServer;

namespace GrpcClient
{
    public class CommunicationGrpcService
    {
        private readonly ILogger<SuppressionGroupChannelRepository> _logger;
        private readonly CommunicationGrpc.CommunicationGrpcClient _communicationGrpcClient;

        public CommunicationGrpcService(ILogger<SuppressionGroupChannelRepository> logger, CommunicationGrpc.CommunicationGrpcClient communicationGrpcClient)
        {
            _communicationGrpcClient = communicationGrpcClient;
            _logger = logger;
        }

        public async Task<CommunicationModel> GetById(int id)
        {
            return await _communicationGrpcClient.GetChannelByIdAsync(new CommunicationChannelRequest() { ChannelId = id });
        }

        public async Task GetAllStreams(CommunicationRequest request)
        {
            using (var call = _communicationGrpcClient.GetAllStream(request))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentDummyGrpc = call.ResponseStream.Current;

                    Console.WriteLine($"{ currentDummyGrpc.ChannelId }: { currentDummyGrpc.Channel }");
                }
            }
        }

        public async Task<CommunicationResponse> GetAll(CommunicationRequest request)
        {
            return await _communicationGrpcClient.GetAllChannelsAsync(request);
        }
    }
}
