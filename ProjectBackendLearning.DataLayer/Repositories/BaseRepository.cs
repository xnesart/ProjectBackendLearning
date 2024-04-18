namespace ProjectBackendLearning.DataLayer.Repositories;

public class BaseRepository
{
    protected readonly MamkinMinerContext _ctx;

    public BaseRepository(MamkinMinerContext context)
    {
        _ctx = context;
    }
}