using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Models.DataTransferObjects
{
    public class MakeDTO : KeyValuePairDTO
    {
        public ICollection<KeyValuePairDTO> Models { get; }

        public MakeDTO()
        {
            Models = new Collection<KeyValuePairDTO>();
        }
    }
}