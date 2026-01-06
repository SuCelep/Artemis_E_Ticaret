using Artemis.DAL.Entities;
using Artemis.UI.Models;

namespace Artemis.UI.ViewModel
{
    public class CheckOutVM
    {
        public Order Order { get; set; }
        public IEnumerable<Card> Cards { get; set; }

        public Product Product { get; set; }
    }
}
