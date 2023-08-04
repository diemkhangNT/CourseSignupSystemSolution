

namespace DaoTaoWebAPI.Models
{
    [Table("MonHoc")]
    public class MonHoc
    {
        [Key]
        public string MaMH { get; set; }

        [Required]
        [StringLength(70)]
        public string TenMH { get; set; }

        [Required]
        [ForeignKey("BoMons")]
        public string MaBM { get; set; }
        public virtual BoMon BoMons { get; set; }
    }
}
