using Hien_Dat_Nha_De2.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hien_Dat_Nha_De2.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> table;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        // Cập nhật hàm GetAll để xử lý Include
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = table;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }

        public T GetById(object id) => table.Find(id);

        public void Insert(T obj) => table.Add(obj);

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            if (existing != null) table.Remove(existing);
        }

        public void Save() => _context.SaveChanges();
    }
}