using Company.Data.Entities;
using Company.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Intranet.Controllers
{
    public class DamageCategoriesController : Controller
    {
        private readonly IDamageCategoryService _damageCategoryService;

        public DamageCategoriesController(IDamageCategoryService damageCategoryService)
        {
            _damageCategoryService = damageCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var damageCategories = await _damageCategoryService.GetAllDamageCategories();
            return View(damageCategories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var damageCategory = await _damageCategoryService.GetDamageCategory(id);

            if (damageCategory is null)
            {
                return NotFound();
            }

            return View(damageCategory);
        }

        public IActionResult Create()
        {
            return View(new DamageCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DamageCategory damageCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(damageCategory);
            }

            await _damageCategoryService.AddDamageCategory(damageCategory);
            TempData["SuccessMessage"] = "Kategoria szkody została dodana.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var damageCategory = await _damageCategoryService.GetDamageCategory(id);

            if (damageCategory is null)
            {
                return NotFound();
            }

            return View(damageCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DamageCategory damageCategory)
        {
            if (id != damageCategory.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(damageCategory);
            }

            await _damageCategoryService.EditDamageCategory(damageCategory);
            TempData["SuccessMessage"] = "Kategoria szkody została zaktualizowana.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var damageCategory = await _damageCategoryService.GetDamageCategory(id);

            if (damageCategory is null)
            {
                return NotFound();
            }

            return View(damageCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _damageCategoryService.DeleteDamageCategory(id);
            TempData["SuccessMessage"] = "Kategoria szkody została usunięta.";

            return RedirectToAction(nameof(Index));
        }
    }
}