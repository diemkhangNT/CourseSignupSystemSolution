
using PhanQuyenWedAPI.Interfaces;
using PhanQuyenWedAPI.Data;
using System.Text.RegularExpressions;

namespace PhanQuyenWedAPI.Services
{
    public class ExistNameService : IExistName
    {
        private readonly ApiDbContext _dbContext;

        public ExistNameService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsCMNDUnique(string cmnd)
        {
            return _dbContext.NhanViens.Any(a => a.CMND == cmnd);
            //throw new NotImplementedException();
        }

        public bool IsEmailUnique(string email)
        {
            return _dbContext.NhanViens.Any(u => u.Email == email);
            //throw new NotImplementedException();
        }

        public bool IsNumber(string sdt)
        {
            string input = sdt;
            bool isNumber = Regex.IsMatch(input, "^[0-9]+$");
            if (isNumber)
            {
                // Biểu thức chính quy kiểm tra số điện thoại
                string pattern = @"^(0|\+84)(\d{9,10})$";

                // Kiểm tra tính hợp lệ của số điện thoại
                return Regex.IsMatch(input, pattern);
            }
            return isNumber;
        }

        public bool IsNumberCMND(string cmnd)
        {
            return Regex.IsMatch(cmnd, "^[0-9]+$"); ;
        }

        public bool IsPassword(string password)
        {
            // Kiểm tra độ dài mật khẩu (ít nhất 8 ký tự)
            if (password.Length < 8)
            {
                return false;
            }
            return true;
        }

        public bool IsTenVT(string tenVT)
        {
            return _dbContext.VaiTros.Any(a => a.TenVT == tenVT);
        }
    }
}
