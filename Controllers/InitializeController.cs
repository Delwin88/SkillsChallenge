using InterviewTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace InterviewTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitializeController : ControllerBase
    {
        private readonly ThreadSafeClass _threadSafeClass;
        public InitializeController(ILogger<PeopleController> logger,ThreadSafeClass threadSafeClass)
        {
            _threadSafeClass = threadSafeClass;
        }

        private void SeedPeople()
        {
            var person = _threadSafeClass.people.ContainsKey(1);
            if (!person)
            {
                List<Person> peopleList = new List<Person>()
                 {
                    new Person()
                    {
                        Id = 1,
                        FirstName = "Jim",
                        LastName = "Parsons",
                        Birthday = new DateTime(1977, 2, 20)
                    },
                    new Person()
                    {
                        Id = 2,
                        FirstName = "Tony",
                        LastName = "Smith",
                        Birthday = new DateTime(1937, 6, 10)
                    }
                };
                _threadSafeClass.people.TryAdd(1, peopleList);
            }

        }

        private void SeedPlaces()
        {
            var placeExists = _threadSafeClass.place.Count > 0;
            if (!placeExists)
            {
                List<Place> placeList = new List<Place>()
                {
                    new Place()
                    {
                        Id = 1,
                        Name = "Big Ben",
                        City = "London",
                        State = "UK"
                    },
                    new Place()
                    {
                        Id = 2,
                        Name = "Willis Tower",
                        City = "Chicago",
                        State = "Illinois"
                    }
                };
                _threadSafeClass.place.AddOrUpdate(1, placeList, (existingValue, oldValue) => placeList);
            }
        }

        private void SeedThings()
        {
            var things = _threadSafeClass.thing.Count > 0;
            if (!things)
            {
                List<Thing> thingsList = new List<Thing>()
                {
                    new Thing()
                    {
                        Id = 1,
                        Name = "Shoe",
                        Description = "It goes on your feet"
                    },
                    new Thing()
                    {
                        Id = 2,
                        Name = "Towel",
                        Description = "You can dry things with it"
                    }
                };
                _threadSafeClass.thing.AddOrUpdate(1, thingsList, (existingValue, oldValue) => thingsList);

            }
        }

        [HttpGet]
        public async Task<ActionResult<bool>> Get()
        {
            SeedPeople();
            SeedPlaces();
            SeedThings();
            return Ok();
        }
    }
}
