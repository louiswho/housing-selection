using System;
using System.Collections.Generic;
using Housing.Selection.Library;
using Xunit;

namespace Housing.Selection.Testing.Library
{
    public class BatchTests
    {
        [Fact]
        public void Batch_ValidState_PassesValidation()
        {
            var batch = new Batch
            {
                Id = Guid.NewGuid(),
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                State = "FL",
                BatchName = ".net-1804",
                BatchOccupancy = 20,
                BatchSkill = ".net",
                Users = new List<User>()
            };

            Assert.True(batch.Validate());
        }

        [Fact]
        public void Batch_InValidState_FailsValidation()
        {
            var batch = new Batch
            {
                Id = Guid.Empty,
                BatchId = Guid.Empty,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                BatchName = "",
                State = "",
                BatchOccupancy = 20,
                BatchSkill = "",
                Users = new List<User>()
            };

            Assert.False(batch.Validate());
        }
    }
}
