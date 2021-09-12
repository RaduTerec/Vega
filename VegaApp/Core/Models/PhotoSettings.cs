using System.IO;
using System.Linq;

namespace Vega.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported (string fileName)
        {
            return AcceptedFileTypes.Any(ft => ft == Path.GetExtension(fileName).ToLower());
        }
    }
}