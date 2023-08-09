using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GhiDanh_DoanhThu.Models
{
    [Table("DoanhThu")]
    public class DoanhThu
    {
        [Key]
        public string MaDT { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ThoiGian { get; set; }

        public double TongDoanhThu { get; set; } = 0;

    }
}
