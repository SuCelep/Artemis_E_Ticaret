using Artemis.DAL.Entities;

namespace Artemis.UI.Areas.admin.ViewModelA
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
 