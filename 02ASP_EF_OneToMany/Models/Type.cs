using _02ASP_EF_OneToMany.Models;

namespace _02ASP_EF_OneToMany.Models
{
    // Вид издания (газета, журнал, альманах, каталог, …)
    // связан отношением "многие к одному" с сущностью Publication
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public List<Edition>? Editions { get; set; }
    }
}
