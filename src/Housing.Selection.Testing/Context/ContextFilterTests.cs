using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;
using System.Collections.Generic;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class ContextFilterTests
    {
        Room TestRoom1;
        Room TestRoom2;
        Room TestRoom3;
        Room TestRoom4;

        List<Room> TestRooms;
        RoomSearchViewModel SearchModel;

        public ContextFilterTests()
        {
            TestRoom1 = new Room
            {
                Location = "Reston",
                Vacancy = 1,
                Occupancy = 4,
                Gender = 'F',
                Address = new Address()
            };

            TestRoom1.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch0"
                    }
                });
            TestRoom1.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch 0"
                    }
                });
            TestRoom1.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch1"
                    }
                });

            TestRoom2 = new Room
            {
                Location = "Reston",
                Vacancy = 3,
                Occupancy = 4,
                Gender = 'M',
                Address = new Address()
            };

            TestRoom2.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch0"
                    }
                });

            TestRoom3 = new Room
            {
                Location = "Tampa",
                Vacancy = 1,
                Occupancy = 4,
                Gender = 'F',
                Address = new Address()
            };

            TestRoom3.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch2"
                    }
                });
            TestRoom3.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch2"
                    }
                });
            TestRoom3.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch3"
                    }
                });

            TestRoom4 = new Room
            {
                Location = "Tampa",
                Vacancy = 4,
                Occupancy = 4,
                Gender = 'M',
                Address = new Address()
            };

            TestRooms.Add(TestRoom1);
            TestRooms.Add(TestRoom2);
            TestRooms.Add(TestRoom3);
            TestRooms.Add(TestRoom4);
        }

        [Fact]
        public void LocationFilter_Reston_TampaFilteredOut()
        {

        }

        [Fact]
        public void LocationFilter_Tampa_RestonFilteredOut()
        {

        }

        [Fact]
        public void BatchFilter_Batch0_FiftyPercent_TestRoom1OnTop()
        {

        }

        [Fact]
        public void BatchFilter_Batch1_OneHundredPercent_AllRoomsFilteredOut()
        {

        }

        [Fact]
        public void GenderFilter_Female_MaleRoomsFilteredOut()
        {

        }

        [Fact]
        public void GenderFilter_Male_FemaleRoomsFilteredOut()
        {

        }

        [Fact]
        public void IsCompletelyUnassignedFilter_True_TestRoom4Returned()
        {

        }

        [Fact]
        public void IsCompletelyUnassignedFilter_False_TestRoom4FilteredOut()
        {

        }
    }
}
