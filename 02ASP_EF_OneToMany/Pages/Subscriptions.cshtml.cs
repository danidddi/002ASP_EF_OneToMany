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
        //�������� ���� ������
        private ApplicationContext _context;
        //��������� ��� ����������� �������
        public List<Edition> DisplayedSubscriptions { get; set; }
        public List<Category>? DisplayedTypes { get; set; }
        //������ ��� ���������� ����� �������� �� �������� �������
        [BindProperty] public Edition CurrentSubscription { get; set; }
        //������ ��� ���������� ������ ���� �������
        [BindProperty] public Category CurrentCategory { get; set; }
        // �������� ������� ��� ����������� ������
        // SelectList(������������������������, �������������������, ����������������������)
        public SelectList CategoryList { get; private set; } = null!;



        //��������� ������������ � ������������
        public SubscriptionsModel(ApplicationContext db) => _context = db;




        //������ �� ��������� ���� ������� �� ���� ������
        public void OnGet()
        {
            DisplayedSubscriptions = _context.Editions.Include(t => t.Category).AsNoTracking().ToList();
            CategoryList = new SelectList(_context.Categorys.AsNoTracking().ToList(), "Id", "CategoryName");
        }

        //���������� ����� ������ ���� �������
        public async Task<IActionResult> OnPostAddCategoryAsync()
        {
            _context.Categorys.Add(CurrentCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("Subscriptions");
        }

        //���������� ����� ������ ��������
        public async Task<IActionResult> OnPostAddSubAsync()
        {
            _context.Editions.Add(CurrentSubscription);
            await _context.SaveChangesAsync();

            return RedirectToPage("Subscriptions");
        }

        //�������� ������
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //������� �� ��������� ������ � ��
            var edition = await _context.Editions.FindAsync(id);

            //���� �����, ������� ������ � ��������� ��������� �� ��
            if (edition != null)
            {
                _context.Editions.Remove(edition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Subscriptions");
        }

        //���������� �� ��������
        public void OnGetByName()
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().OrderBy(x => x.Name).ToList();
        //���������� �� ���������
        public void OnGetByCost()
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().OrderBy(x => x.Cost).ToList();
        //����� �� �������
        public void OnPostFindByIndex(string searchIndex)
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Index.Equals(searchIndex)).ToList();
        //����� �� ���������
        public void OnPostFindByCost(int min, int max)
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Cost >= min && x.Cost <= max).ToList();
        //����� �� ������������ ��������
        public void OnPostFindByDuration(int min, int max)
            => DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Duration >= min && x.Duration <= max).ToList();
        //����� �� ���� �������
        public void OnPostFindByCategory(int searchCategory)
        {
            //��� ���������� ������� ��� ������� ������ ��� ��������
            CategoryList = new SelectList(_context.Categorys.AsNoTracking().ToList(), "Id", "CategoryName");
            DisplayedSubscriptions = _context.Editions.Include(c => c.Category).AsNoTracking().Where(x => x.Category.Id == searchCategory).ToList();
        }
    }
}

