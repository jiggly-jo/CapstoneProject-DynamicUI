using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace Smartroom.Models
{
    public class UHospitalContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<RoomButton> roomButton { get; set; }
        public DbSet<Click> clicks{ get; set; }
        public DbSet<RoomButtonCategory> roomButtonCategory { get; set; }
        public UHospitalContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "u_hospital.db");
            optionsBuilder
                .UseSqlite($"Data Source={filename}");
        }
    }
}
