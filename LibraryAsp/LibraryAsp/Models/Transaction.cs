using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAsp.Models
{
    public class Transaction
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_transaction { get; set; }

        public int id_user { get; set; }

        public int id_book { get; set; }

        public DateTime start_time { get; set; }

        public DateTime end_time { get; set; }

        public int status { get; set; }

        public DateTime createdAt { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }
    }
}