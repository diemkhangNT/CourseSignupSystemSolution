using GhiDanh_DoanhThu.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSignupSystemServer.Models
{
    [Table("DangKy")]
    public class DangKy
    {
        [Key]
        public string MaDK { get; set; }

        [Required]
        public string MaHV { get; set; }

        [Required]
        public string MaLop { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayDK { get; set; }

        
        [ForeignKey("HocPhis")]
        public string? MaHP { get; set; }
        public virtual HocPhi HocPhis { get; set; }
    }
}
