using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.DAL.Entities
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name ="Üst Kategori Adı")]
        public int? ParentID { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set;}

        [Column(TypeName ="varchar(50)"), StringLength(50),Display(Name ="Kategori")]
        public string Name { get; set; }
        [Display(Name ="Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
