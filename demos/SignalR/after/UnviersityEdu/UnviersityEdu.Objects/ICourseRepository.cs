using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;

namespace UnviersityEdu.Objects
{
    public interface ICourseRepository : IRepository<Course>
    {
        //IEnumerable<Course> GetActiveCourses();
        //IEnumerable<Course> GetCoursesByPage(int page, int amount);

    }

    public static class CourseQueries
    {
        public static IQueryable<Course>
            HasAtLeastCredits(this IQueryable<Course> query, int credits)
        {
            return query.Where(course => course.Credits > credits);
        }
    }

    public interface ICourseQueryBuilder
    {
        ICourseQueryBuilder HasAtLeastCredits(int credits);
        ICourseQueryBuilder HasTitleLike(string title);

        IEnumerable<Course> Execute();
    }

   

    public interface IRepository<T>
    {
        IQueryable<Course> All { get; }
        void Add(T course);
        void Remove(T course);


    }
}