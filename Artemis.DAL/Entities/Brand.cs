using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.DAL.Entities
{
    public class Brand
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Marka Adı"), Required(ErrorMessage ="Marka Adı Boş Bırakılamaz")]
        public string Name { get; set; }
        public int DisplayIndex { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
