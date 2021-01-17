using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class Orders
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "The Order Name field is required"), Column(TypeName = "nvarchar(255)")]
        public string OrderName { get; set; }
        public string Name { get; set; }
       
        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }
        public string ReceivedDate { get; set; }

        [DefaultValue(1)]
        public int Status { get; set; }
    
        public string address { get; set; }
        public string message { get; set; }
        [Display(Name ="phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string telephone { get; set; }
        public int paymenttype { get; set; }

        public decimal GrandTotal { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]

        public string UserID { get; set; }
        public Orders()
        {
            OrderProducts = new Collection<OrderProducts>();

        }

        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
