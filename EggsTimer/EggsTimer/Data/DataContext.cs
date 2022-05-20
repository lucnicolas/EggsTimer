using System;
using System.IO;
using EggsTimer.Models;
using Microsoft.EntityFrameworkCore;

namespace EggsTimer.Data
{
    public class DataContext : DbContext
    {
        private static readonly string directory =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public DataContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<TimerModel> Timers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var filePath = Path.Combine(directory, "Notepad.db3");
            optionsBuilder.UseSqlite($"Filename={filePath}");
        }
    }
}