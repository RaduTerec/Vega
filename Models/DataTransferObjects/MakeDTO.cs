using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Models.DataTransferObjects
{
    public class MakeDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<ModelDTO> Models { get; }

        public MakeDTO()
        {
            Models = new Collection<ModelDTO>();
        }
    }
}