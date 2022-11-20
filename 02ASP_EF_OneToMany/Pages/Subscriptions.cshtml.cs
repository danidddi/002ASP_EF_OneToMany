using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _02ASP_EF_OneToMany.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _02ASP_EF_OneToMany.Pages
{
    [IgnoreAntiforgeryToken]
    public class SubscriptionsModel : PageModel
    {
        //контекст базы данных
        private ApplicationContext _context;
        //коллекци€ дл€ отображени€ записей
        public List<Edition> DisplayedSubscriptions { get; set; }
        public List<Category>? DisplayedTypes { get; set; }
        //объект дл€ добавлени€ новой подписки на печатное издание
        [BindProperty] public Edition CurrentSubscription { get; set; }
        //объект дл€ добавлени€ нового вида издани€
        [BindProperty] public Category CurrentCategory { get; set; }
        // создание объекта дл€ выпадающего списка
        // SelectList(коллекци€ƒанныхƒл€¬ывода, »м€—войства«начени€, »м€—войстваќтображени€)
        public SelectList CategoryList { get; private set; } = null!;



        //внедрение зависимостей в конструкторе
        public SubscriptionsModel(ApplicationContext db) => _context = db;




        //запрос на получение всех записей из базы данных
        public void OnGet()
        {
            DisplayedSubscriptions = _context.Editions.Include(t => t.Category).AsNoTracking().ToList();
            CategoryList = new SelectList(_context.Categorys.AsNoTracking().ToList(), "Id", "CategoryName");
        }

        //добавление новой записи вида издани€
        public async Task<IActionResult> OnPostAddCategoryAsync()
        {
            _context.Categorys.Add(CurrentCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("Subscriptions");
        }

        //добавление новой записи подписки
        public async Task<IActionResult> OnPostAddSubAsync()
        {
            _context.Editions.Add(CurrentSubscription);
            await _context.SaveChangesAsync();

            return RedirectToPage("Subscriptions");
        }

        //удаление записи
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //находим по айдишнику запись в бд
            var edition = await _context.Editions.FindAsync(id);

            //если нашли, удал€ем запись и сохран€ем изменени€ на бд
            if (edition != null)
            {
                _context.Editions.Remove(edition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Subscriptions");
        }

        //сортировка по названию
        public void OnGetByName()
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().OrderBy(x => x.Name).ToList();
        //сортировка по стоимости
        public void OnGetByCost()
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().OrderBy(x => x.Cost).ToList();
        //поиск по индексу
        public void OnPostFindByIndex(string searchIndex)
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Index.Equals(searchIndex)).ToList();
        //поиск по стоимости
        public void OnPostFindByCost(int min, int max)
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Cost >= min && x.Cost <= max).ToList();
        //поиск по длительности подписки
        public void OnPostFindByDuration(int min, int max)
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Duration >= min && x.Duration <= max).ToList();
        //поиск по виду издани€
        public void OnPostFindByCategory(int searchCategory)
        {
            //без повторного запроса нет пунктов списка дл€ фильтраы
            CategoryList = new SelectList(_context.Categorys.AsNoTracking().ToList(), "Id", "CategoryName");
            DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Category.Id == searchCategory).ToList();
        }
    }
}

