namespace GiangVienWebAPI.Interfaces
{
    public interface IExistName
    {
        bool IsEmailGVUnique(string emailGV); // email giảng viên
        bool IsCMNDgvUnique(string cmndGV); // chứng minh nhân dân của giảng viên
        bool IsNumber(string sdt);
        bool IsNumberCMND(string cmnd);
        bool IsPassword(string password);
    }
}
