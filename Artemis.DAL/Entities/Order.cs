using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.DAL.Entities
{
    public class Order
    {
        public int ID { get; set; }

        [Column(TypeName = "Varchar(20)"), StringLength(20), Display(Name = "Sipariş Numarası")]
        public string OrderNumber { get; set; }

        [Display(Name = "Ödeme Seçeneği")]
        public EPaymentOption PaymentOption { get; set; }

        [Display(Name = "Sipariş Durumu")]
        public EOrderStatus OrderStatus { get; set; }


        [Display(Name = "Sipariş Tarihi")]
        public DateTime RecDate { get; set; }


        [Column(TypeName = "varchar(100)"), StringLength(100), Display(Name = "Teslimat Adresi")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Teslimat Ülkesi")]
        public string Country { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Teslimat Şehri")]
        public string City { get; set; }


        [Column(TypeName = "varchar(5)"), StringLength(5), Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Adı Soyadı")]
        public string NameSurname { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

        [NotMapped, Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }
        [NotMapped]
        public string CardMonth { get; set; }
        [NotMapped]
        public string CarddYear { get; set; }
        [NotMapped, Display(Name = "CV2 Kodu")]
        public string CardCv2 { get; set; }

    }
}
