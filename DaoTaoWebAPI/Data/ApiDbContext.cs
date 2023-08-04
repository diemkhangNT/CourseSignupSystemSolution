using DaoTaoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DaoTaoWebAPI.Data
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
        //private readonly HttpContext _httpContext;

        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<BoMon> BoMons { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<Diem> Diems { get; set; }
        public virtual DbSet<LoaiDiem_MH> LoaiDiem_MHs { get; set; }
        public virtual DbSet<NienKhoa> NienKhoas { get; set; }
        public virtual DbSet<LoaiDiem> LoaiDiems { get; set; }


        //Set up primary key
        public override int SaveChanges()
        {
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
            foreach (var entry in ChangeTracker.Entries().Where(e=>e.State == EntityState.Added))
            {
                if (entry.Entity is LopHoc lopHoc)
                {
                    string num0 = new string(Enumerable.Repeat(chars, 4).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    string num = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    lopHoc.MaLop = "HP" + num0 + "-" + num;
                    
                }
                else if (entry.Entity is BoMon boMon)
                {
                    string num1 = new string(Enumerable.Repeat(chars, 9).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    boMon.MaBM = "BM" + "_" + num1;
                }
                else if (entry.Entity is LoaiDiem loaiDiem)
                {
                    string num2 = new string(Enumerable.Repeat(chars, 4).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    loaiDiem.MaLDiem = "LD" + "_" + num2;
                }
                else if (entry.Entity is MonHoc monHoc)
                {
                    string num3 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    monHoc.MaMH = "MH" + "_" + num3;
                }
                else if (entry.Entity is Khoa khoa)
                {
                    string num6 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    khoa.MaKhoa = "KO" + "_" + num6;
                }
                else if (entry.Entity is NienKhoa nienKhoa)
                {
                    string num6 = new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    nienKhoa.MaNK = "NK" + "_" + num6;
                }
            }
                return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DangKy>().HasKey(dk => new { dk.MaLop, dk.MaHV });
            modelBuilder.Entity<LoaiDiem_MH>().HasKey(lv => new { lv.MaMH, lv.MaLDiem });
            modelBuilder.Entity<Diem>().HasKey(bd => new { bd.MaHV, bd.MaLopHoc});
            base.OnModelCreating(modelBuilder);

            ////set no delete no action on LopHoc table
            modelBuilder.Entity<LopHoc>().HasOne(t => t.Khoas).WithMany().HasForeignKey(t => t.MaKhoa).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LopHoc>().HasOne(t => t.MonHocs).WithMany().HasForeignKey(t => t.MaMH).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.DoanhThus).WithMany().HasForeignKey(t => t.MaDT).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.LichNghis).WithMany().HasForeignKey(t => t.MaLichNghi).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.GiangViens).WithMany().HasForeignKey(t => t.MaGV).OnDelete(DeleteBehavior.Restrict);

            ////set default value
            //modelBuilder.Entity<LoaiDiem>().Property(e => e.HeSo).HasDefaultValue(1);
        }
    }
}
