using GhiDanh_DoanhThu.Data;
using GhiDanh_DoanhThu.Interface;
using GhiDanh_DoanhThu.Models;

namespace GhiDanh_DoanhThu.Services
{
    public class TinhDoanhThu : IDoanhThu
    {
        private readonly APIDBContext _dbContext;

        public TinhDoanhThu(APIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsThoiGian(DateTime thoiGian)
        {
            return _dbContext.DoanhThus.Any(u => u.ThoiGian == thoiGian);
        }

        public double TinhDT(DateTime thoiGian)
        {
            double tongDT = 0;
            foreach (var item in _dbContext.HocPhis)
            {
                DateTime dt = item.NgayThu;
                int thang = dt.Month;
                int nam = dt.Year;
                int inputThang = thoiGian.Month;
                int inputNam = thoiGian.Year;
                if (item.TrangThai && thang == inputThang && nam == inputNam)
                {
                    tongDT += item.PhaiThu;
                }
                
            }
            return tongDT;
        }
    }
}
