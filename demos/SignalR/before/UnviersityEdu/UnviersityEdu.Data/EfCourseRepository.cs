using System.Data.Entity;
using System.Linq;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Data
{
    public class EfCourseRepository : ICourseRepository
    {
        private readonly DbContext ctx;

        public EfCourseRepository(DbContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<Course> All
        {
            get { return ctx.Set<Course>(); }
        }

        public void Add(Course course)
        {
            ctx.Set<Course>().Add(course);
        }

        public void Remove(Course course)
        {
            ctx.Set<Course>().Remove(course);
        }

        public IQueryable<Course> GetActiveCourses()
        {
            throw new System.NotImplementedException();
        }
    }
}