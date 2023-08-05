namespace GiangVienWebAPI.Models
{
    [Table("GiangVien")]
    public class GiangVien
    {
        [Key]
        public string MaGV { get; set; }

        [Required]
        [StringLength(50)]
        public string TenGV { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(10)]
        public string SDTLienLac { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(12)]
        public string CMND { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; } = true;

        public string? HinhDaiDien { get; set; }

        [Required]
        public DateTime NgayHopTac { get; set; }

        public bool TrangThaiHD { get; set; } = true;

        public string? MaBM { get; set; }

        //[Required]
        //[ForeignKey("Luongs")]
        //public string MaLuong { get; set; }
        //public virtual Luong Luongs { get; set; }
    }
}
