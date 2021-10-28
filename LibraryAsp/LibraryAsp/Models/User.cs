using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAsp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_user { get; set; }

        [StringLength(255)]
        [Required]
        public string email { get; set; }

        [StringLength(255)]
        [Required]
        public string password { get; set; }

        [StringLength(255)]
        [Required]
        public string fullname { get; set; }

        [StringLength(255)]
        [Required]
        public string address { get; set; }

        public int gender { get; set; }

        [StringLength(255)]
        [Required]
        public string phone { get; set; }

        public DateTime birthday { get; set; }

        public int id_role { get; set; }

        public virtual Role Role { get; set; }
    }
}