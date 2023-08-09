using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanQuyenWedAPI.Models
{
    [Table("LoaiNguoiDung")]
    public class NguoiDung
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [ForeignKey("VaiTro")]
        public string MaVT { get; set; }
        public virtual VaiTro VaiTro { get; set; }
    }
}
