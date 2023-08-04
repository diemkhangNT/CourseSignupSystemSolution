using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HocVienWebAPI.Data;

namespace HocVienWebAPI.Models
{
    [Table("LienHe")]
    public class LienHe
    {
        [Key]
        public string MaLH { get; set; }
        [Required]
        [StringLength(50)]
        public string TieuDe { get; set; }
        [Required]
        [StringLength(200)]
        public string NoiDung { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayLH { get; set; }
        [Required]
        [ForeignKey("HocViens")]
        public string MaHV { get; set; }
        public virtual HocVien HocViens { get; set; }

    }
}