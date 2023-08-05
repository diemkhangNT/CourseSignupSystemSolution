
using System.Text.RegularExpressions;

namespace GiangVienWebAPI.Services
{
    public class ExistNameService : IExistName
    {
        private readonly ApiDbContext _dbContext;

        public ExistNameService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsCMNDgvUnique(string cmndGV)
        {
            return _dbContext.GiangViens.Any(a => a.CMND == cmndGV);
            //throw new NotImplementedException();
        }

        public bool IsEmailGVUnique(string emailGV)
        {
            return _dbContext.GiangViens.Any(u => u.Email == emailGV);
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
    }
}
