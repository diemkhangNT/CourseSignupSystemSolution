namespace BlazorWebServerUI.Models
{
    public class LoaiDiem_MH
    {
        public string MaLDiem { get; set; }
        public virtual LoaiDiem LoaiDiems { get; set; }
        public string MaMH { get; set; }
    }
}
