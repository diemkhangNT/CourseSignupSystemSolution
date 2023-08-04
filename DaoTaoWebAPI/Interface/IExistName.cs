namespace DaoTaoWebAPI.Interfaces
{
    public interface IExistName
    {
        bool IsTenLDiemUnique(string tenLDiem); // tên loại điểm
        bool IsTenMHUnique(string tenMH); // tên môn học
        bool IsTenBMUnique(string tenBM); // tên bộ môn
        bool IsTenKhoaUnique(string tenKhoa); // tên khoa
        bool IsThoiGiannkUnique(string thoiGianNK); // thời gian của niên khóa (2022-2023)
    }
}
