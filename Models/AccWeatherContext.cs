using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Weather.Models
{
    public partial class AccWeatherContext : DbContext
    {
        public AccWeatherContext()
        {
        }

        public AccWeatherContext(DbContextOptions<AccWeatherContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FavoritesCity> FavoritesCities { get; set; }
        public virtual DbSet<LocationWeather> LocationWeathers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-KBRCI1R\\SQLEXPRESS;Initial Catalog=AccWeather;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<FavoritesCity>(entity =>
            {
                entity.HasKey(e => e.CityKey);

                entity.ToTable("favoritesCity");

                entity.Property(e => e.CityKey)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LocalizedName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LocationWeather>(entity =>
            {
                entity.HasKey(e => e.CityKey);

                entity.ToTable("LocationWeather");

                entity.Property(e => e.CityKey)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.LocalizedName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WeatherText)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
