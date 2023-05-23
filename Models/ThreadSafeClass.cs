using System.Collections.Concurrent;

namespace InterviewTest.Models
{
    public class ThreadSafeClass
    {
        public ConcurrentDictionary<int, List<Person>> people = new ConcurrentDictionary<int, List<Person>>();      
        public ConcurrentDictionary<int, List<Thing>> thing = new ConcurrentDictionary<int, List<Thing>>();       
        public ConcurrentDictionary<int, List<Place>> place = new ConcurrentDictionary<int, List<Place>>();
    }
}
