namespace PlatformService.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<T> Repository<T>() where T : class, new();
        IPlatformRepo platformRepo { get; }
    }
}
