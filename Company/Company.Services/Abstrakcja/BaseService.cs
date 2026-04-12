using Company.Data;
using Company.Data.Data;

namespace Company.Services.Abstrakcja
{
    public abstract class BaseService
    {
        protected readonly MechanicDbContext _context;

        public BaseService(MechanicDbContext context)
        {
            _context = context;
        }
    }
}