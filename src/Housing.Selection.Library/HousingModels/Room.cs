using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{
    public class Room
    {
        /// <summary>
        /// Our primary key.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        [Required]
        public Guid RoomId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Vacancy { get; set; }
        
        [Required]
        public int Occupancy { get; set; }

        [Required]
        public string Gender { get; set; }
        
        [Required]
        public Address Address { get; set; }

        public ICollection<User> Users { get; set; }

        public Room ConvertFromServiceModel(ApiRoom apiRoom)
        {
            Room housingRoom = this;            
            housingRoom.RoomId = apiRoom.RoomId;
            housingRoom.Location = apiRoom.Location;
            housingRoom.Address = apiRoom.Address.ConvertToAddress(housingRoom.Address);            
            housingRoom.Vacancy = apiRoom.Vacancy;
            housingRoom.Occupancy = apiRoom.Occupancy;
            housingRoom.Gender = apiRoom.Gender;          
            
            return housingRoom;
        }

        public double BatchPercentage(string batchName)
        {
            var batchUsers = Users.Where(x => x.Batch.BatchName == batchName);
            return batchUsers.Count() / Users.Count;
        }
    }
}
