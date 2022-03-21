namespace FptBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class category
    {
        public category()
        {
            books = new HashSet<book>();
        }
        [Required]
        [StringLength(10)]
        public string categoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string categoryName { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public ICollection<book> books { get; set; }
    }
}
