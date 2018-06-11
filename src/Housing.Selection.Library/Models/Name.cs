using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library.Models
{
    /// <summary>
    /// The Name class.
    /// Contains the properties of Revature associate Names.
    /// </summary>
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

        /// <summary>The First  property represents the first name of the associate.</summary>
        /// <value>The first property gets/sets the value of the string field, first.</value>
        [Required]
        public string First { get; set; }

        /// <summary>The Middle property represents the middle name of the associate.</summary>
        /// <value>The middle property gets/sets the value of the string field, first.</value>
        public string Middle { get; set; }

        /// <summary>The middle property represents the last name of the associate.</summary>
        /// <value>The last property gets/sets the value of the string field, last.</value>
        [Required]
        public string Last { get; set; }

        /// <summary>The Users property represents a collection of all Revature associates.</summary>
        /// <value>The Users property gets/sets the value of the Collection of User objects.</value>
        public ICollection<User> Users { get; set; }
    }
}
