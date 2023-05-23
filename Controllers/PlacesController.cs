using InterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InterviewTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly ThreadSafeClass _threadSafeClass;

        private readonly ILogger<PlacesController> _logger;

        public PlacesController(ILogger<PlacesController> logger, ThreadSafeClass threadSafeClass)
        {
            _logger = logger;
            _threadSafeClass = threadSafeClass;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> Get(string? filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return _threadSafeClass.place.Values.SelectMany(placeList => placeList)
                    .Where(placename => placename.Name.ToLower().Contains(filter.ToLower()) || placename.City.ToLower().Contains(filter.ToLower()) ||
                    placename.State.ToLower().Contains(filter.ToLower())).ToList();
            }

            return  _threadSafeClass.place.Values.SelectMany(placeList => placeList).ToList();
        }
    }
}
