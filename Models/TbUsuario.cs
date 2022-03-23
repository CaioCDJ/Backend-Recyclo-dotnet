using System;
using System.Collections.Generic;

namespace Backend_Recyclo_dotnet.Models
{
    public partial class TbUsuario
    {
        public TbUsuario()
        {
            TbDenuncia = new HashSet<TbDenuncium>();
        }

        public int CdUsuario { get; set; }
        public string? NmUsuario { get; set; }
        public string? CdSenha { get; set; }
        public string? CdCpf { get; set; }
        public string DsEmail { get; set; } = null!;
        public string? CdTelefone { get; set; }

        public virtual ICollection<TbDenuncium> TbDenuncia { get; set; }
    }
}
