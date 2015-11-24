namespace UnviersityEdu.Objects
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; } 
        // ...

        void Commit();
    }
}