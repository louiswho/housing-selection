using System;
using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;
using Xunit;

namespace Housing.Selection.Testing.Library
{
    public class UserTests
    {
        [Fact]
        public void User_ValidData_PassesValidation()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Location = "Tampa",
                Email = "mike@mike.com",
                Gender = "M",
                Batch = new Batch(),
                Room = new Room(),
                Name = new Name(),
                Address = new Address()
            };

            Assert.True(user.Validate());
        }

        [Fact]
        public void User_InvalidData_FailsValidation()
        {
            var user = new User
            {
                Id = Guid.Empty,
                UserId = Guid.Empty,
                Location = "",
                Email = "mike.mike.com",
                Gender = "AH",
                Batch = new Batch(),
                Room = new Room(),
                Name = new Name(),
                Address = new Address()
            };

            Assert.False(user.Validate());
        }
    }
}
