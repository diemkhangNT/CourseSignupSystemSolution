namespace GiangVienWebAPI.Models
{
    public class PhanCongGiangDay
    {
        [Key]
        [ForeignKey("GiangViens")]
        public string MaGV { get; set; }
        public virtual GiangVien GiangViens { get; set; }
        
        [Key]   
        public string MaLop { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayBatDau { get; set; }

        public string ThoiGianDay { get; set; }
    }
}
