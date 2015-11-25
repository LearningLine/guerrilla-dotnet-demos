using UnviersityEdu.Objects;

namespace UnviersityEdu.Data
{
    public class EfUnitofWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new SchoolContext();
        }
    }
}