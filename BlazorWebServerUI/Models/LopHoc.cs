

namespace BlazorWebServerUI.Models
{
    public class LopHoc
    {
        public string MaLop { get; set; }

        public string TenLop { get; set; }

        public bool TrangThai { get; set; }

        public double HocPhi { get; set; }

        public DateTime NgayKhaiGiang { get; set; }

        public string ThoiGianHoc { get; set; } // T3-T5 17:00-21:00
        
        public string Phong { get; set; }
        
        public int SoBuoiHoc { get; set; }
        
        public string MaKhoa { get; set; }
       
        public string MaMH { get; set; }

        public string? MaLichNghi { get; set; }

        public string? MaGV { get; set; }

    }
}
