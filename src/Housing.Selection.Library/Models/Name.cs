using System;

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
        public Guid Id { get; set; }

        /// <summary>The NameId property represents the Primary Key  of the associate  for the Housing-Selection database Table.</summary>
        /// <value>The NameId property gets/sets the value of the Guid field, NameId.</value>
        public Guid NameId { get; set; }

        /// <summary>The First  property represents the first name of the associate.</summary>
        /// <value>The first property gets/sets the value of the string field, first.</value>
        public string First { get; set; }

        /// <summary>The Middle property represents the middle name of the associate.</summary>
        /// <value>The middle property gets/sets the value of the string field, first.</value>
        public string Middle { get; set; }

        /// <summary>The middle property represents the last name of the associate.</summary>
        /// <value>The last property gets/sets the value of the string field, last.</value>
        public string Last { get; set; }
    }
}
