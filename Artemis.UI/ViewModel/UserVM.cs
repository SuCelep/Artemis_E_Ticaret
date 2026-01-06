using Artemis.DAL.Entities;

namespace Artemis.UI.ViewModel
{
    public class UserVM
    {
        public User Users { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}
