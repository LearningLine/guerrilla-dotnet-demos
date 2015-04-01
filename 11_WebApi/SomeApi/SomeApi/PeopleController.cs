using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace SomeApi
{
    // DTO
    public class Person
    {
        [Required]
        public string FullName { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Age { get; set; }
    }

    [RoutePrefix("people")]
    public class PeopleController : ApiController
    {
        static List<Person> people = new List<Person>()
        {
            new Person{FullName = "Brock", Age = 12},
            new Person{FullName = "Alice", Age = 42},
            new Person{FullName = "Bob Loblaw", Age = 56},
        };

        // GET ~/people
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            //return StatusCode((HttpStatusCode)418);
            return Ok(people);
        }
        
        // GET ~/people/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            if (people.Count <= id || id < 0)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Not Found, dummy" }));
            }

            return Ok(people[id]);
        }

        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            var q =
                from p in people
                where p.FullName == name
                select p;
            var person = q.FirstOrDefault();
            if (person == null) return NotFound();

            return Ok(person);
        }


        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Put(int id, Person model)
        {
            if (people.Count <= id || id < 0)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Not Found, dummy" }));
            }

            //if (ModelState.IsValid)
            //{
            //    if(model.Age < 0)
            //    {
            //        ModelState.AddModelError("age", "Age must be greater than 0");
            //    }
            //}

            if(ModelState.IsValid == false)
            {
                var errors =
                    from s in ModelState
                    where s.Value.Errors.Any()
                    from e in s.Value.Errors
                    select !String.IsNullOrWhiteSpace(e.ErrorMessage) ? e.ErrorMessage : e.Exception.Message;

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { errors=errors.ToArray() }));
                //return BadRequest(ModelState);
                //return StatusCode(HttpStatusCode.BadRequest);
            }

            var p = people[id];
            p.FullName = model.FullName;
            p.Age = model.Age;

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}