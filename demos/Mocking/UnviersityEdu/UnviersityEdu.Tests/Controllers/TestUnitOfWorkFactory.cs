using UnviersityEdu.Objects;

namespace UnviersityEdu.Tests.Controllers
{
	public class TestUnitOfWorkFactory : IUnitOfWorkFactory
	{
		public IUnitOfWork Create()
		{
			return new TestUnitOfWork();
		}
	}
}