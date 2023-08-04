

using HocVienWebAPI.Data;
using HocVienWebAPI.Interfaces;
using System.Text.RegularExpressions;

namespace HocVienWebAPI.Services
{
    public class IndexValidService : IIndexValid
    {
        private readonly ApiDbContext _dbContext;

        public IndexValidService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsEmailUnique(string emailHV)
        {
            return _dbContext.HocViens.Any(u => u.Email == emailHV);
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
