using System.Threading.Tasks;
using GrpcClient;
using GrpcServer;

public class SuppressionGroupChannelRepository
{
    private readonly CommunicationGrpcService _service;
    public SuppressionGroupChannelRepository(CommunicationGrpcService service)
    {
        _service = service;
    }

    public async Task<CommunicationModel> GetModel(int id)
    {
        return await _service.GetById(id);
    }

    public async Task<CommunicationResponse> GetAll()
    {
        return await _service.GetAll(new CommunicationRequest());
    }
}
