using Artemis.DAL.Entities;

namespace Artemis.UI.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Slide> Slides { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
