using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using Polling.DataAccess;
using Polling.Entities;

namespace Polling.WebAPI.Controllers
{
    public class PollsOldController : ODataController
    {
        private readonly IPollsRepository repo;

        public PollsOldController()
        {
            repo = new PollsRepository(new PollingContext());
        }
        public PollsOldController(IPollsRepository repo)
        {
            this.repo = repo;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Poll> GetAll()
        {
            return repo.All;
        }

        [EnableQuery]
        public SingleResult<Poll> Get([FromODataUri] int key)
        {
            IQueryable<Poll> result = repo.All.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        public IHttpActionResult GetById(int id)
        {
            var poll = repo.GetById(id);
            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }
        
    }
}
