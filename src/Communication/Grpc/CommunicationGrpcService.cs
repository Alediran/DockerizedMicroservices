using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CommunicationGrpcService : CommunicationGrpc.CommunicationGrpcBase
    {
        public List<CommunicationModel> mockModel;
        private readonly ILogger<CommunicationGrpcService> _logger;

        public CommunicationGrpcService(ILogger<CommunicationGrpcService> logger)
        {
            _logger = logger;
            mockModel = new List<CommunicationModel>
            {
                new CommunicationModel
                {
                    ChannelId = 1,
                    Channel = "CA"
                },
                new CommunicationModel
                {
                    ChannelId = 1,
                    Channel = "CC"
                }
            };
        }

        public override Task<CommunicationModel> GetChannelById(CommunicationChannelRequest request, ServerCallContext context)
        {
            CommunicationModel output = new CommunicationModel();

            output = mockModel.Find(x => x.ChannelId == request.ChannelId);

            context.Status = new Status(StatusCode.OK, $"ok");

            return Task.FromResult(output);
        }

        public override async Task GetAllStream(
            CommunicationRequest request, 
            IServerStreamWriter<CommunicationModel> responseStream, 
            ServerCallContext context)
        {
            foreach (var model in mockModel)
            {
                await responseStream.WriteAsync(model);
            }
        }

        public override Task<CommunicationResponse> GetAllChannels(CommunicationRequest request, ServerCallContext context)
        {
            var model = new CommunicationResponse();
            model.Response.AddRange(mockModel);
            model.Total = mockModel.Count;

            context.Status = new Status(StatusCode.OK, $"ok");

            return Task.FromResult(model);
        }
    }
}
