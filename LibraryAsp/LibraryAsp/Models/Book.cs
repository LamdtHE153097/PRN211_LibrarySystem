using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAsp.Models
{
    public class Book
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_book { get; set; }

        [StringLength(255)]
        [Required]
        public string name { get; set; }

        [StringLength(255)]
        [Required]
        public string author { get; set; }

        public int id_publisher { get; set; }

        public int id_category { get; set; }

        public int year_publish { get; set; }

        public float price { get; set; }

        public string description { get; set; }

        [StringLength(255)]
        [Required]
        public string image { get; set; }

        public DateTime createdAt { get; set; }

        public int quantity { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual Category Category { get; set; }
    }
}