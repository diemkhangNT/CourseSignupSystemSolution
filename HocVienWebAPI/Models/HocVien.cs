using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HocVienWebAPI.Models
{
    [Table("HocVien")]
    public class HocVien
    {
        [Key]
        public string MaHV { get; set; }

        [Required]
        [StringLength(50)]
        public string TenHV { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength (50)]
        public string PhuHuynh { get; set; }

        [StringLength (10)]
        public string SDTLienLac { get; set; }

        public string? AnhDaiDien { get; set; }
    }
}
