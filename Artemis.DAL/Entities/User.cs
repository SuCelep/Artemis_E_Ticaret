using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Kullanıcı Adı"), Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz")]
        public string UserName {get; set; }

        [Column(TypeName = "varchar(32)"), StringLength(32), Display(Name = "Şifre"), Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Ad Soyad"), Required(ErrorMessage = "Ad Soyad Boş Bırakılamaz")]
        public string NameSurname { get; set; }

        [Display(Name ="Durum")]
        public bool Status { get; set; }

        [Display(Name ="Son Giriş Tarihi")]
        public DateTime LastLoginDate { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
