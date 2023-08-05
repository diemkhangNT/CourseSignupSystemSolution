using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GhiDanh_DoanhThu.Models
{
    [Table("HocPhi")]
    public class HocPhi
    {
        [Key]
        public string MaHP { get; set; }

        [Required]
        public string LoaiHP { get; set; }
        
        public DateTime NgayThu { get; set; }

        [Required]
        public double GiamGia { get; set; }

        [Required]
        public double PhaiThu { get; set; }

        public string? GhiChu { get; set; }

    }
}
