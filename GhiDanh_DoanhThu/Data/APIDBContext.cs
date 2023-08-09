using CourseSignupSystemServer.Models;
using GhiDanh_DoanhThu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace GhiDanh_DoanhThu.Data
{
    public class APIDBContext : DbContext
    {
        public APIDBContext(DbContextOptions<APIDBContext> options) : base(options)
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

        public virtual DbSet<DangKy> DangKies { get; set; }
        public virtual DbSet<HocPhi> HocPhis { get; set; }
        public virtual DbSet<DoanhThu> DoanhThus { get; set; }


        //Set up primary key
        public override int SaveChanges()
        {
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity is DangKy dangKy)
                {
                    string num = new string(Enumerable.Repeat(chars, 9).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    string num1 = new string(Enumerable.Repeat(chars, 9).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    dangKy.MaDK = "DK" + num + "-" + num1;

                }
                else if (entry.Entity is HocPhi hocPhi)
                {
                    string num = new string(Enumerable.Repeat(chars, 10).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    hocPhi.MaHP = "HP" + "_" + num;

                }
                else if (entry.Entity is DoanhThu doanhThu)
                {
                    DateTime now = DateTime.Now;
                    doanhThu.MaDT = "DT" + "_" + now.ToString("yyyyMMddHHmmss");

                }
            }
            return base.SaveChanges();
        }
    }
}
