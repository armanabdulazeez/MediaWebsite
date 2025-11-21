using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Data.Entities;

namespace WebAppFreelanceMediaApi.Data.DbEntities
{
    public class MVPDbContext : DbContext
    {
        public MVPDbContext() { }

        public MVPDbContext(DbContextOptions<MVPDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.HasIndex(u => u.UserName)
                    .IsUnique();

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar");

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(s => s.StaffId);

                entity.Property(s => s.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.HasIndex(s => s.UserName)
                    .IsUnique();

                entity.Property(s => s.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(s => s.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(s => s.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar");

                entity.Property(s => s.PhoneNumber)
                     .IsRequired();

                entity.Property(s => s.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(ar => ar.ArticleId);

                entity.Property(ar => ar.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar");

                entity.Property(ar => ar.Body)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(ar => ar.Category)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnType("varchar");

                entity.Property(ar => ar.ImagePath)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(ar => ar.PostedDate)
                    .HasColumnType("date");

                entity.HasOne(ar => ar.User)
                    .WithMany(u => u.Articles)
                    .HasForeignKey(ar => ar.UserId);
            });

            modelBuilder.Entity<Ad>(entity =>
            {
                entity.HasKey(a => a.AdId);

                entity.Property(a => a.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar");

                entity.Property(a => a.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(a => a.Category)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnType("varchar");

                entity.Property(a => a.ImagePath)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.HasOne(a => a.Staff)
                    .WithMany(s => s.Ads)
                    .HasForeignKey(a=>a.StaffId);
            });
        }
    }
}
