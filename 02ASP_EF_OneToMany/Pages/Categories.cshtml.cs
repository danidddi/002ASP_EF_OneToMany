using _02ASP_EF_OneToMany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _02ASP_EF_OneToMany.Pages
{
    public class CategoriesModel : PageModel
    {
        public ApplicationContext Context { get; set; }
        public List<Category> DisplayedCategories { get; set; }
        public Category EditedCategory { get; set; }

        public CategoriesModel(ApplicationContext context)
        {
            Context = context;
        }

        public void OnGet() 
            => DisplayedCategories = Context.Categorys.AsNoTracking().ToList();

        public void OnGetEdite(int id)
        {
            EditedCategory = Context.Categorys.Find(id);
        }

        public void OnPostSave()
        {
            Context.Categorys.Update(EditedCategory);
            Context.SaveChanges();
        }

    }
}
