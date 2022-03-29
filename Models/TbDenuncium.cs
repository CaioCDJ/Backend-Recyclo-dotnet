using System.ComponentModel.DataAnnotations.Schema;


namespace Backend_Recyclo_dotnet.Models
{
    public partial class TbDenuncium
    {
        public int CdDenuncia { get; set; }
        public string? NmLogradouro { get; set; }
        public string? DsComentario { get; set; }
        [Column(TypeName="Date")]
        public DateTime? DtDenuncia { get; set; }
        public int FkCdDiscarteIlegal { get; set; }
        public int FkCdUsuario { get; set; }

        public virtual TbDiscarteIlegal FkCdDiscarteIlegalNavigation { get; set; } = null!;
        public virtual TbUsuario FkCdUsuarioNavigation { get; set; } = null!;
    }
}
