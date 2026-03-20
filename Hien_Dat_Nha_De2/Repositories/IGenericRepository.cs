using System.Linq.Expressions;

namespace Hien_Dat_Nha_De2.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // Thêm tham số mảng params expression để Include các bảng liên kết
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}