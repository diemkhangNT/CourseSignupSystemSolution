using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanQuyenWedAPI.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public string MaNV { get; set; }

        [Required]
        [StringLength(50)] 
        public string TenNV { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(10)] 
        public string SDT { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        [Range(9,12)]
        public string CMND { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; } = true;

        public string? HinhDaiDien { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayVaoLam { get; set; }

        public bool TrangThaiHD { get; set; } = true;

        [Required]
        [ForeignKey("NguoiDungs")]
        public string MaND { get; set; }
        public virtual NguoiDung NguoiDungs { get; set; }

    }
}
