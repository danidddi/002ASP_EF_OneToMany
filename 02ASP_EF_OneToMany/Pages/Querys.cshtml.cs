using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using _02ASP_EF_OneToMany.Models;

namespace _02ASP_EF_OneToMany.Pages
{
    public class QuerysModel : PageModel
    {
        private ApplicationContext _context;
        public List<Query5>? Query5 { get; set; }
        public List<Query6>? Query6 { get; set; }
        public List<Query7>? Query7 { get; set; }
        

        public QuerysModel(ApplicationContext context)
        {
            _context = context;
        }

        public void OnGetQuery5()
        {
            Query5 = _context.Editions
                .AsNoTracking()
                .GroupBy(x => x.Category.CategoryName, (key, group) =>
                    new Query5(key, (int)group.Average(x => x.Cost), group.Count()))
                .ToList();
        }

        public void OnGetQuery6()
        {
            Query6 = _context.Editions
                .AsNoTracking()
                .GroupBy(x => x.Category.CategoryName, (key, group) =>
                    new Query6(key, group.Min(x => x.Cost), group.Max(x => x.Cost), group.Count()))
                .ToList();
        }        
        
        public void OnGetQuery7()
        {
            Query7 = _context.Editions
                .AsNoTracking()
                .GroupBy(x => x.Duration, (key, group) =>
                    new Query7(key, (int)group.Average(x => x.Cost)))
                .ToList();
        }
    }
}

public record Query5 (string type, int avg, int amnt);
public record Query6 (string type, int min, int max, int amnt);
public record Query7 (int duration, int cost);