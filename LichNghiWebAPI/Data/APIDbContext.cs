using System.Collections.Generic;
using LichNghiWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace LichNghiWebAPI.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //private readonly HttpContext _httpContext;

        public virtual DbSet<LichNghi> LichNghis { get; set; }


        //Set up primary key
        public override int SaveChanges()
        {
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity is LichNghi lichNghi)
                {
                    string num = new string(Enumerable.Repeat(chars, 9).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    lichNghi.MaLN = "LN" + "_" + num;

                }
            }
            return base.SaveChanges();
        }
    }
}
