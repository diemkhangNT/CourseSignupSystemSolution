using HocVienWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace HocVienWebAPI.Data
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

        public virtual DbSet<HocVien> HocViens { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }


        //Set up primary key
        public override int SaveChanges()
        {
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
            foreach (var entry in ChangeTracker.Entries().Where(e=>e.State == EntityState.Added))
            {
                if (entry.Entity is HocVien hocVien)
                {
                    string num = new string(Enumerable.Repeat(chars, 16).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    hocVien.MaHV = "HV" + "_" + num;
                    
                }
                else if (entry.Entity is LienHe lienHe)
                {
                    string num = new string(Enumerable.Repeat(chars, 10).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    lienHe.MaLH = "LH" + "_" + num;

                }
            }
                return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DangKy>().HasKey(dk => new { dk.MaLop, dk.MaHV });
            //modelBuilder.Entity<LichVang>().HasKey(lv => new { lv.MaLop, lv.MaHV });
            //modelBuilder.Entity<Diem>().HasKey(bd => new { bd.MaMH, bd.MaHV, bd.MaLDiem });
            //base.OnModelCreating(modelBuilder);

            ////set no delete no action on LopHoc table
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.Khoas).WithMany().HasForeignKey(t => t.MaKhoa).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.MonHocs).WithMany().HasForeignKey(t => t.MaMH).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.DoanhThus).WithMany().HasForeignKey(t => t.MaDT).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.LichNghis).WithMany().HasForeignKey(t => t.MaLichNghi).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<LopHoc>().HasOne(t => t.GiangViens).WithMany().HasForeignKey(t => t.MaGV).OnDelete(DeleteBehavior.Restrict);

            ////set default value
            //modelBuilder.Entity<LoaiDiem>().Property(e => e.HeSo).HasDefaultValue(1);
        }
    }
}
