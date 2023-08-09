using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanQuyenWedAPI.Models
{
    [Table("VaiTro")]
    public class VaiTro
    {
        [Key]
        public string MaVT { get; set; }
        [Required]
        public string TenVT { get; set; }
    }
}
