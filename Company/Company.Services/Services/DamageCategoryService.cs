using Company.Data;
using Company.Data.Data;
using Company.Data.Entities;
using Company.Interfaces.Services;
using Company.Services.Abstrakcja;
using Microsoft.EntityFrameworkCore;

namespace Company.Services.Services
{
    public class DamageCategoryService : BaseService, IDamageCategoryService
    {
        public DamageCategoryService(MechanicDbContext context)
            : base(context)
        {
        }

        public async Task<IList<DamageCategory>> GetAllDamageCategories()
        {
            var damageCategories = await _context.DamageCategories
                .OrderBy(damageCategory => damageCategory.Name)
                .ToListAsync();

            return damageCategories;
        }

        public async Task<DamageCategory?> GetDamageCategory(int id)
        {
            var damageCategory = await _context.DamageCategories
                .FirstOrDefaultAsync(damageCategory => damageCategory.Id == id);

            return damageCategory;
        }

        public async Task AddDamageCategory(DamageCategory damageCategory)
        {
            _context.DamageCategories.Add(damageCategory);
            await _context.SaveChangesAsync();
        }

        public async Task EditDamageCategory(DamageCategory damageCategory)
        {
            var damageCategoryFromDatabase = await _context.DamageCategories
                .FirstOrDefaultAsync(damageCategoryItem => damageCategoryItem.Id == damageCategory.Id);

            if (damageCategoryFromDatabase is null)
            {
                return;
            }

            damageCategoryFromDatabase.Name = damageCategory.Name;
            damageCategoryFromDatabase.Description = damageCategory.Description;
            damageCategoryFromDatabase.IsActive = damageCategory.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteDamageCategory(int id)
        {
            var damageCategory = await _context.DamageCategories
                .FirstOrDefaultAsync(damageCategoryItem => damageCategoryItem.Id == id);

            if (damageCategory is null)
            {
                return;
            }

            _context.DamageCategories.Remove(damageCategory);
            await _context.SaveChangesAsync();
        }
    }
}