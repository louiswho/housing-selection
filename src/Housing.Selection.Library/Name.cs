using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library.Models
{

   
   public  class Name
    {
        /// <summary>The Id property represents the Primary Key of the associate  for the Housing-Selection database Table.</summary>
        /// <value>The Id property gets/sets the value of the Guid field, Id.</value>
        [Key]
        public Guid Id { get; set; }

        /// <summary>The NameId property represents the Primary Key  of the associate  for the ServiceHub database Table.</summary>
        /// <value>The NameId property gets/sets the value of the Guid field, NameId.</value>
        [Required]
        public Guid NameId { get; set; }

       
        [Required]
        public string First { get; set; }

        public string Middle { get; set; }

        [Required]
        public string Last { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
