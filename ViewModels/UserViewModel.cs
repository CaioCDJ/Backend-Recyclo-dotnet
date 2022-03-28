using System.ComponentModel.DataAnnotations;

namespace Backend_Recyclo_dotnet.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string? NmUsuario { get; set; }
        [Required]
        public string? CdSenha { get; set; }
        [Required]
        public string? CdCpf { get; set; }
        [Required]
        public string? DsEmail { get; set; }
        public string? CdTelefone { get; set; }
    }
}