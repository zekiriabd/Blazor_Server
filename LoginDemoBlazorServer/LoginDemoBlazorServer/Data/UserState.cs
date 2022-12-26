using System.ComponentModel.DataAnnotations;

namespace LoginDemoBlazorServer.Data
{
    public class UserState
    {
        [Required(ErrorMessage = "Veuillez d'indiquer votre identifiant"), MaxLength(20)]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Veuillez d'indiquer votre mot de passe"), MaxLength(20)]
        public string? Password { get; set; }
        public bool IsAuthenticate { get; set; }
        public string? Role { get; set; }
        public string? FullName { get; set; }
    }
}
