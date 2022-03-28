using System.ComponentModel.DataAnnotations;

namespace Backend_Recyclo_dotnet.ViewModels
{
    public class EmpresaViewModel
    {
        [Required]
        public string? NmEmpresa { get; set; }
        [Required]
        public string? CdCnpj { get; set; }
        public string? CdTelefone { get; set; }
        [Required]
        public string? NmEmail { get; set; }
        [Required]
        public string? CdSenhaEmpresa { get; set; }

    }
}