using Microsoft.JSInterop;

namespace BehaviorTracker.Client.Models
{
    public class Copyable<T>
    {
        public T Copy()
        {
            var content = Json.Serialize(this);
            return Json.Deserialize<T>(content);
        }
    }
}