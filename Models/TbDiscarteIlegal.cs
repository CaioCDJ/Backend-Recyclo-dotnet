using System;
using System.Collections.Generic;

namespace Backend_Recyclo_dotnet.Models
{
    public partial class TbDiscarteIlegal
    {
        public TbDiscarteIlegal()
        {
            TbDenuncia = new HashSet<TbDenuncium>();
        }

        public int CdDiscarte { get; set; }
        public string? CdLocalizacao { get; set; }

        public virtual ICollection<TbDenuncium> TbDenuncia { get; set; }
    }
}
