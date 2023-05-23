using InterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ThreadSafeClass _threadSafeClass;


        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ILogger<PeopleController> logger, ThreadSafeClass threadSafeClass)
        {
            _logger = logger;
            _threadSafeClass = threadSafeClass;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get(string? filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return _threadSafeClass.people.Values
                    .SelectMany(personList => personList) // Flatten the list of lists into a single list
                    .Where(person => person.LastName.ToLower().Contains(filter.ToLower()) || person.FirstName.ToLower().Contains(filter.ToLower()))
                    .ToList();
            }
            HttpContext.Response.Headers.Remove("X-Powered-By");
            return _threadSafeClass.people.Values.SelectMany(personList => personList).ToList();
        }

        //[HttpPost]
        //public void Post(List<Person> input)
        //{
        //    if (input!= null)
        //    {
        //        _threadSafeClass.people.TryAdd(2,input);
        //    }

            
        //}
    }
}
