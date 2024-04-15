namespace ProjectBackendLearning.DataLayer.Repositories;

public class BaseRepository
{
    protected readonly string _connectionString;

    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
}