using System;
using System.Collections.Generic;
using Housing.Selection.Library;
using Xunit;

namespace Housing.Selection.Testing.Library
{
    public class AddressTests
    {
        [Fact]
        public void Address_ValidState_PassesValidation()
        {
            var address = new Address
            {
                Id = Guid.NewGuid(),
                AddressId = Guid.NewGuid(),
                Address1 = "123 Me St.",
                Address2 = "#31",
                City = "Tampa",
                State = "FL",
                PostalCode = "91711",
                Country = "US",
                Batches = new List<Batch>(),
                Users = new List<User>(),
                Rooms = new List<Room>()
            };

            Assert.True(address.Validate());
        }

        [Fact]
        public void Address_InValidState_FailsValidation()
        {
            var address = new Address
            {
                Id = Guid.Empty,
                AddressId = Guid.Empty,
                Address1 = "",
                Address2 = "",
                City = "",
                State = "",
                PostalCode = "",
                Country = "",
                Batches = new List<Batch>(),
                Users = new List<User>(),
                Rooms = new List<Room>()
            };

            Assert.False(address.Validate());
        }
    }
}
