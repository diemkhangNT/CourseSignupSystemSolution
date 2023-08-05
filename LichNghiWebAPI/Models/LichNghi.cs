using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LichNghiWebAPI.Models
{
    [Table("LichNghi")]
    public class LichNghi
    {
        [Key]
        public string MaLN { get; set; } 

        [Required]
        public string TenLN { get; set; } 

        public string? MoTa { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayBD { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayKT { get; set; }
    }
}
