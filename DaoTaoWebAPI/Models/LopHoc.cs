

namespace DaoTaoWebAPI.Models
{
    [Table("LopHoc")]
    public class LopHoc
    {
        [Key]
        public string MaLop { get; set; }

        [Required]
        public string TenLop { get; set; }

        [Required]
        public bool TrangThai { get; set; }

        [Required]
        [Range(0, 10000000)]
        public double HocPhi { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayKhaiGiang { get; set; }

        [Required]
        public string ThoiGianHoc { get; set; } // T3-T5 17:00-21:00
        [Required]
        public string Phong { get; set; }
        [Required]
        public int SoBuoiHoc { get; set; }
        [Required]
        [ForeignKey("Khoas")]
        public string MaKhoa { get; set; }
        public virtual Khoa Khoas { get; set; }
        [Required]
        [ForeignKey("MonHocs")]
        public string MaMH { get; set; }
        public virtual MonHoc MonHocs { get; set; }

        [Required]
        public string? MaLichNghi { get; set; }

        [Required]
        public string? MaGV { get; set; }

    }
}
