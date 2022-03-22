namespace FptBook.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class book
    {
        public book()
        {
            orderDetails = new HashSet<orderDetail>();
        }
        [Required]
        [StringLength(10)]
        public string bookID { get; set; }

        [Required]
        [StringLength(100)]
        public string bookName { get; set; }

        [Required]
        [StringLength(10)]
        public string categoryID { get; set; }

        [Required]
        [StringLength(10)]
        public string authorID { get; set; }

        [Required(ErrorMessage = "Price can not be empty!")]
        [Range(0, 1000, ErrorMessage = "Please enter number less 1000")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "Price can not be empty!")]
        [Range(0, 1000, ErrorMessage = "Please enter number less 1000")]
        public int price { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Book Image")]
        public string image { get; set; }

        [Required]
        [StringLength(200)]
        public string shortDesc { get; set; }

        [Required]
        [StringLength(500)]
        public string detailDesc { get; set; }

        public virtual author author { get; set; }

        public virtual category category { get; set; }


        public ICollection<orderDetail> orderDetails { get; set; }
    }
}
