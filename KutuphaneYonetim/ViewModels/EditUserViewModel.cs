using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetim.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } 

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
    }
}