using System;
using System.Collections.Generic;

namespace Backend_Recyclo_dotnet.Models
{
    public partial class TbPontoColetum
    {
        public int CdPontoColeta { get; set; }
        public string? NmPonto { get; set; }
        public string? NmLogradouro { get; set; }
        public int? FkCdEmpresa { get; set; }
        public string? CdLatitudePonto { get; set; }
        public string? CdLongitudePonto { get; set; }

        public virtual TbEmpresa? FkCdEmpresaNavigation { get; set; }
    }
}
