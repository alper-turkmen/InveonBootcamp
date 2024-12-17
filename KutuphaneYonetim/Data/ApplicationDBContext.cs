using KutuphaneYonetim.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KutuphaneYonetim.Data
{
    public class AppDbContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    Author = "George Orwell",
                    PublicationYear = 1949,
                    ISBN = "1111111111",
                    Genre = "Roman",
                    Publisher = "Can Yayinlari",
                    PageCount = 328,
                    Language = "İngilizce",
                    Summary = "Totaliter bir rejim üzerine bir roman",
                    AvailableCopies = 5,
                    CoverImagePath = "/images/1984.jpg"
                },
                new Book
                {
                    Id = 2,
                    Title = "Savaş ve Barış",
                    Author = "Lev Tolstoy",
                    PublicationYear = 1869,
                    ISBN = "222222222",
                    Genre = "Tarih",
                    Publisher = "Ani Yayinlari",
                    PageCount = 1225,
                    Language = "Rusça",
                    Summary = "Napolyon dönemi üzerine bir destan.",
                    AvailableCopies = 3,
                    CoverImagePath = "/images/savasvebaris.jpg"
                },
                new Book
                {
                    Id = 3,
                    Title = "Dönüşüm",
                    Author = "Franz Kafka",
                    PublicationYear = 1915,
                    ISBN = "3333333333",
                    Genre = "Roman",
                    Publisher = "Yapi Kredi Yayinlari",
                    PageCount = 89,
                    Language = "Almanca",
                    Summary = "Bir sabah Gregor Samsa, bir böceğe dönüşmüş olarak uyanır.",
                    AvailableCopies = 2,
                    CoverImagePath = "/images/donusum.jpg"
                },
                new Book
                {
                    Id = 4,
                    Title = "Küçük Prens",
                    Author = "Antoine de Saint-Exupéry",
                    PublicationYear = 1943,
                    ISBN = "4444444444",
                    Genre = "Çocuk",
                    Publisher = "Is Bankasi Yayinlari",
                    PageCount = 96,
                    Language = "Fransızca",
                    Summary = "Bir çocuğun gözünden dünyayı anlatan bir masal.",
                    AvailableCopies = 4,
                    CoverImagePath = "/images/kucukprens.jpg"
                }
            );
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } 
    }
}