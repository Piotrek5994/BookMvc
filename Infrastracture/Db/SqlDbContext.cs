using Core.Enums;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Db
{
    public class SqlDbContext : IdentityDbContext<User, UserRole, int>
    {
        public DbSet<Book> books { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<PublishingHouse> publishingHouses { get; set; }
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite("Data Source= C:\\Users\\piotr\\Desktop\\Projekty C#\\BookMvc\\book.db");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId);

                entity.HasOne(b => b.PublishingHouse)
                      .WithMany(p => p.Books)
                      .HasForeignKey(b => b.PublishingHouseId);
            });
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasMany(a => a.Books)
                      .WithOne(b => b.Author)
                      .HasForeignKey(a => a.AuthorId);
            });
            modelBuilder.Entity<PublishingHouse>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasMany(p => p.Books)
                      .WithOne(b => b.PublishingHouse)
                      .HasForeignKey(p => p.PublishingHouseId);
            });
            modelBuilder.Entity<Book>().HasData(
                new Book() { Id = 1, Title = "Dziady", Description = "Lektura", PageNumber = 77, ReleaseDate = new DateTime(2024, 2, 22), Category = Category.Action, AuthorId = 1, PublishingHouseId = 1 },
                new Book() { Id = 2, Title = "Hobbit", Description = "Lektura", PageNumber = 100, ReleaseDate = new DateTime(2024, 2, 20), Category = Category.Fantasies, AuthorId = 2, PublishingHouseId = 2 });

            modelBuilder.Entity<Author>().HasData(
                new Author() { Id = 1, FirstName = "Organizacja A", LastName = "1234567890", DateOfBirth = new DateTime(2020, 05, 15) },
                new Author() { Id = 2, FirstName = "Organizacja B", LastName = "0987654321", DateOfBirth = new DateTime(2020, 03, 12) });

            modelBuilder.Entity<PublishingHouse>().HasData(
                new PublishingHouse() { Id = 1, Name = "Tygodnik", Address = "Kraków" },
                new PublishingHouse() { Id = 2, Name = "Gazeta", Address = "Warszawa" }
            );
        }
    }
}
