
namespace BlazorWebServerUI.Models
{
    public class NhanVien
    {
        public string MaNV { get; set; }

        public string TenNV { get; set; }

        public string Email { get; set; }

        
        public string Password { get; set; }

        public string SDT { get; set; }

        public string DiaChi { get; set; }

        public string CMND { get; set; }

        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; } = true;

        public string? HinhDaiDien { get; set; }

        public DateTime NgayVaoLam { get; set; }

        public bool TrangThaiHD { get; set; } = true;

        public string MaND { get; set; }

    }
}
