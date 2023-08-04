namespace DaoTaoWebAPI.Models
{
    [Table("LoaiDiem")]
    public class LoaiDiem
    {
        [Key]
        public string MaLDiem { get; set; }
        [Required]
        public string TenLDiem { get; set; } // Mieng, 15 phut, 1 tiet
        [Range(0.1, 2.0)]
        public double HeSo { get; set; }
    }
}
