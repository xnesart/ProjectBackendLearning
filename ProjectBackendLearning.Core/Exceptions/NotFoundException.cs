namespace ClassLibrary1ProjectBackendLearning.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}