
namespace DaoTaoWebAPI.Services
{
    public class ExistNameService : IExistName
    {
        private readonly ApiDbContext _dbContext;

        public ExistNameService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsTenBMUnique(string tenBM)
        {
            return _dbContext.BoMons.Any(u => u.TenBM == tenBM);
            //throw new NotImplementedException();
        }

        public bool IsTenKhoaUnique(string tenKhoa)
        {
            return _dbContext.Khoas.Any(u => u.TenKhoa == tenKhoa);
            //throw new NotImplementedException();
        }

        public bool IsTenLDiemUnique(string tenLDiem)
        {
            return _dbContext.LoaiDiems.Any(u => u.TenLDiem == tenLDiem);
            //throw new NotImplementedException();
        }

        public bool IsTenMHUnique(string tenMH)
        {
            return _dbContext.MonHocs.Any(u => u.TenMH == tenMH);
            //throw new NotImplementedException();
        }

        public bool IsThoiGiannkUnique(string thoiGianNK)
        {
            return _dbContext.NienKhoas.Any(u => u.ThoiGian == thoiGianNK);
            //throw new NotImplementedException();
        }
    }
}
