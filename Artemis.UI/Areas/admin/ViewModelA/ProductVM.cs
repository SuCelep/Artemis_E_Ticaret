using Artemis.DAL.Entities;

namespace Artemis.UI.Areas.admin.ViewModelA
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<Category>Categories { get; set; }
        public IEnumerable<Brand>Brands { get; set; }
        public IEnumerable<User>Users { get; set; }
    }
}
