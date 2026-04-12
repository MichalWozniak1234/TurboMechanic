using Company.Data.Entities;

namespace Company.Interfaces.Services
{
    public interface IDamageCategoryService
    {
        Task<IList<DamageCategory>> GetAllDamageCategories();

        Task<DamageCategory?> GetDamageCategory(int id);

        Task AddDamageCategory(DamageCategory damageCategory);

        Task EditDamageCategory(DamageCategory damageCategory);

        Task DeleteDamageCategory(int id);
    }
}