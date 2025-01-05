
using System.ComponentModel.DataAnnotations;

    public class UpdateProfileDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(500)]
        public string About { get; set; }
    }

    public class UpdateProfilePictureDto
    {
        [Required]
        public string ProfilePicture { get; set; }
    }