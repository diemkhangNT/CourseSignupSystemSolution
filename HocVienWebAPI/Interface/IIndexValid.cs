namespace HocVienWebAPI.Interfaces
{
    public interface IIndexValid
    {
        bool IsEmailUnique(string emailHV); // email học viên
        bool IsNumber(string sdt);
        bool IsPassword(string password);
    }
}
