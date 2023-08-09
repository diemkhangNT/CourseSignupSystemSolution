namespace GiangVienWebAPI.Models
{
    public class GiangVien
    {
        public string MaGV { get; set; }

        public string TenGV { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string SDTLienLac { get; set; }

        public string DiaChi { get; set; }

        public string CMND { get; set; }

        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; } = true;

        public string? HinhDaiDien { get; set; }

        public DateTime NgayHopTac { get; set; }

        public bool TrangThaiHD { get; set; } = true;

        public string? MaBM { get; set; }
    }
}
