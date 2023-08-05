using Microsoft.EntityFrameworkCore;
using GiangVienWebAPI.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace GiangVienWebAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) 
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
        
        public virtual DbSet<GiangVien> GiangViens { get; set; }


        //Set up primary key
        public override int SaveChanges()
        {
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
            foreach (var entry in ChangeTracker.Entries().Where(e=>e.State == EntityState.Added))
            {
                if (entry.Entity is GiangVien giangVien)
                {
                    DateTime now = DateTime.Now;
                    string num7 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    string num8 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    giangVien.MaGV = "GV" + "_" + num7 + "_" + num8;
                    giangVien.NgayHopTac = now;
                        
                }
            }
                return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhanCongGiangDay>().HasKey(dk => new { dk.MaLop, dk.MaGV });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GiangVienWebAPI.Models.PhanCongGiangDay>? PhanCongGiangDay { get; set; }
    }
}
