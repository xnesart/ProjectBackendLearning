namespace ProjectBackendLearning.DataLayer.Repositories;

public class BaseRepository
{
    protected readonly BackMinerContext _ctx;

    public BaseRepository(BackMinerContext context)
    {
        _ctx = context;
    }
}