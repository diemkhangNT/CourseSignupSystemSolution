using Microsoft.EntityFrameworkCore;
using PhanQuyenWedAPI.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace PhanQuyenWedAPI.Data
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
        
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }


        //Set up primary key
        public override int SaveChanges()
        {
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
            foreach (var entry in ChangeTracker.Entries().Where(e=>e.State == EntityState.Added))
            {
                if (entry.Entity is NhanVien nhanVien)
                {
                    DateTime now = DateTime.Now;
                    string num7 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    string num8 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    nhanVien.MaNV = "NV" + "_" + num7 + "_" + num8;
                    nhanVien.NgayVaoLam = now;
                        
                }
                else if (entry.Entity is NguoiDung nguoiDung)
                {
                    DateTime now = DateTime.Now;
                    string num9 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    nguoiDung.UserId = "NV" + "_" + num9;

                }
            }
                return base.SaveChanges();
        }
    }
}
