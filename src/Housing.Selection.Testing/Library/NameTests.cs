using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;
using Xunit;

namespace Housing.Selection.Testing.Library
{
    public class NameTests
    {
        [Fact]
        public void Name_ValidState_PassesValidation()
        {
            var name = new Name
            {
                Id = Guid.NewGuid(),
                NameId = Guid.NewGuid(),
                First = "Mike",
                Middle = "Mike",
                Last = "Mike",
                Users = new List<User>()
            };

            Assert.True(name.Validate());
        }

        [Fact]
        public void Name_InValidState_FailsValidation()
        {
            var name = new Name
            {
                Id = Guid.Empty,
                NameId = Guid.Empty,
                First = "",
                Middle = "",
                Last = "",
                Users = new List<User>()
            };

            Assert.False(name.Validate());
        }
    }
}
