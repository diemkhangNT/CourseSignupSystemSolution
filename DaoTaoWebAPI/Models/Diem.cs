
namespace DaoTaoWebAPI.Models
{
    [Table("Diem")]
    public class Diem
    {
        [Key] //[ForeignKey("HocViens")]
        public string MaHV { get; set; }

        [Key]
        [ForeignKey("LopHocs")]
        public string MaLopHoc { get; set; }
        public virtual LopHoc LopHocs { get; set; }

        [Range(0.0, 10.0)]
        public double SoDiem { get; set; }
    }
}
