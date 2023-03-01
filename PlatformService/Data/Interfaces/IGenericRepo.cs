namespace PlatformService.Data.Interfaces
{
    public interface IGenericRepo<T> where T : class, new()
    {
        Task<T> Create(T entity);
        Task<T> Find(string id);
        Task<IReadOnlyList<T>> FindAll();
        Task Remove(T entity);
        Task<T> Update(T entity);
    }
}
