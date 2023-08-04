namespace DaoTaoWebAPI.Models
{
    public class LoaiDiem_MH
    {
        [Key]
        [ForeignKey("LoaiDiems")]
        public string MaLDiem { get; set; }
        public virtual LoaiDiem LoaiDiems { get; set; }

        [Key]
        [ForeignKey("MonHocs")]
        public string MaMH { get; set; }
        public virtual MonHoc MonHocs { get; set; }
    }
}
