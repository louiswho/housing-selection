using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{ 
   public  class Name
    {
        /// <summary>
        /// Our primary key.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        [Required]
        public Guid NameId { get; set; }

        [Required]
        public string First { get; set; }

        public string Middle { get; set; }

        [Required]
        public string Last { get; set; }

        public ICollection<User> Users { get; set; }

        public static explicit operator ApiName(Name name)
        {
            ApiName apiName = new ApiName()
            {
                NameId = name.NameId,
                First = name.First,
                Middle = name.Middle,
                Last = name.Last,
                
            };
            return apiName;
        }
    }
}
