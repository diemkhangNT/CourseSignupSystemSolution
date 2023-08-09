namespace PhanQuyenWedAPI.Interfaces
{
    public interface IExistName
    {
        bool IsEmailUnique(string email); // email
        bool IsCMNDUnique(string cmnd); // chứng minh nhân dân 
        bool IsNumber(string sdt);
        bool IsNumberCMND(string cmnd);
        bool IsPassword(string password);
        bool IsTenVT(string tenVT);
    }
}
