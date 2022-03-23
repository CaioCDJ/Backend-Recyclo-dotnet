using System;
using System.Collections.Generic;

namespace Backend_Recyclo_dotnet.Models
{
    public partial class TbEmpresa
    {
        public TbEmpresa()
        {
            TbPontoColeta = new HashSet<TbPontoColetum>();
        }

        public int CdEmpresa { get; set; }
        public string? NmEmpresa { get; set; }
        public string? CdCnpj { get; set; }
        public string? CdTelefone { get; set; }
        public string? NmEmail { get; set; }
        public string? CdSenhaEmpresa { get; set; }

        public virtual ICollection<TbPontoColetum> TbPontoColeta { get; set; }
    }
}
