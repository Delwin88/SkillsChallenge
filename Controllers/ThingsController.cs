using InterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingsController : ControllerBase
    {
        private readonly ILogger<ThingsController> _logger;
        private readonly ThreadSafeClass _threadSafeClass;

        public ThingsController(ILogger<ThingsController> logger, ThreadSafeClass threadSafeClass)
        {
            _logger = logger;
            _threadSafeClass = threadSafeClass;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Thing>>> Get(string? filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return _threadSafeClass.thing.Values.SelectMany(thingList => thingList)
                    .Where(thingName => thingName.Name.ToLower().Contains(filter.ToLower()) ||
                    thingName.Description.ToLower().Contains(filter.ToLower())).ToList();
            }

            return _threadSafeClass.thing.Values.SelectMany(thingList => thingList).ToList();
        }
    }
}
