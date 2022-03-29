using System.ComponentModel.DataAnnotations;

namespace Backend_Recyclo_dotnet.ViewModels
{
    public class DenunciaViewModel
    {
                [Required]
        public int CdDenuncia { get; set; }
        [Required]
        public string? NmLogradouro { get; set; }
        [Required]
        public string? DsComentario { get; set; }
        public DateTime? DtDenuncia { get; set; } = DateTime.Now;
        [Required]
        public int FkCdDiscarteIlegal { get; set; }
        [Required]
        public int FkCdUsuario { get; set; }
    }
}