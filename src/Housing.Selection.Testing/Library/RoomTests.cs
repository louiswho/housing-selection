using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library;
using Xunit;

namespace Housing.Selection.Testing.Library
{
    public class RoomTests
    {
        [Fact]
        public void Room_ValidState_PassesValidation()
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                Location = "Tampa",
                Vacancy = 3,
                Occupancy = 5,
                Gender = "M",
                Address = new Address(),
                Users = new List<User>()
            };

            Assert.True(room.IsValidState());
        }

        [Fact]
        public void Room_InValidState_FailsValidation()
        {
            var room = new Room
            {
                Id = Guid.Empty,
                RoomId = Guid.Empty,
                Location = "",
                Vacancy = 6,
                Occupancy = 5,
                Gender = "AH",
                Address = new Address(),
                Users = new List<User>()
            };

            Assert.False(room.IsValidState());
        }
    }
}
