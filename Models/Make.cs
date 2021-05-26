using System.Collections.Generic;

namespace Vega.Models
{
    public class Make
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Model> Models { get; }
    }
}
