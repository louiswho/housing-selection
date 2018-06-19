using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{
    public class Room
    {
        private const int MaxStringLength = 255;
        /// <summary>
        /// Our primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        public Guid RoomId { get; set; }

        public string Location { get; set; }

        public int Vacancy { get; set; }

        public int Occupancy { get; set; }

        public string Gender { get; set; }

        public Address Address { get; set; }

        public ICollection<User> Users { get; set; }

        public bool IsValidState()
        {
            if (RoomId == Guid.Empty) { return false; }
            if (string.IsNullOrEmpty(Location) || Location?.Length > MaxStringLength || Location?.Length <= 0) { return false; }
            if (Occupancy <= 0) { return false; }
            if (Vacancy > Occupancy || Vacancy < 0) { return false; }
            if (Gender == null) { return false; }

            return true;
        }

        /// <summary>
        /// Use this method to convert from a 
        /// service hub Room, to a housing Room
        /// leaves nav properties the same
        /// </summary>
        /// <param name="apiRoom">An ApiRoom object is passed into this method.
        /// Updates the housing Room properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
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
