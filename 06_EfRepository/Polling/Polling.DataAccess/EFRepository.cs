using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Polling.DataAccess
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        private DbSet<T> set;

        public EFRepository(DbContext context)
        {
            this.context = context;

            this.set = context.Set<T>();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Add(T item)
        {
            set.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return set.ToArray();
        }

        public virtual T GetById(int id)
        {
            return set.Find(id);
        }

        public void Remove(T item)
        {
            set.Remove(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}