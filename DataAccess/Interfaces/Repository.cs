namespace DataAccess.Interfaces
{
    public interface Repository<T, C>
    {
        List<T> GetAll();
        T GetById(C id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> Search(string keyword);
    }
}
