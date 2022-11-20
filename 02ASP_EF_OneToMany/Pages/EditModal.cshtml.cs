using _02ASP_EF_OneToMany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _02ASP_EF_OneToMany.Pages
{
    [IgnoreAntiforgeryToken]
    public class EditModalModel : PageModel
    {
        private ApplicationContext _context;
        [BindProperty] public Edition Edition { get; set; }
        public SelectList CategoryList { get; private set; } = null!;

        public EditModalModel(ApplicationContext db) => _context = db;


        public async Task<IActionResult> OnGetAsync(int id)
        {
            CategoryList = new SelectList(_context.Categorys.AsNoTracking().ToList(), "Id", "CategoryName");
            Edition = await _context.Editions.FindAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostEditeAsync()
        {

            _context.Editions.Update(Edition);
            await _context.SaveChangesAsync();
            return RedirectToPage("Subscriptions");
        }
    }
}
