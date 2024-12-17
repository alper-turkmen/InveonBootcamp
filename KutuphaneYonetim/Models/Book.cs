using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetim.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Author { get; set; } 

        public int PublicationYear { get; set; }

        [MaxLength(20)]
        public string ISBN { get; set; } 

        [MaxLength(50)]
        public string Genre { get; set; } 

        [MaxLength(100)]
        public string Publisher { get; set; }

        public int PageCount { get; set; }

        [MaxLength(50)]
        public string Language { get; set; } 

        [MaxLength(1000)]
        public string Summary { get; set; } 

        public int AvailableCopies { get; set; }

        public string CoverImagePath { get; set; }
    }
}