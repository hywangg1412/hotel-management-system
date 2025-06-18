namespace Bussiness.Interfaces
{
    public interface Service<T, C>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAll();
        T GetById(C id);
        List<T> Search(string keyword);
    }
}
