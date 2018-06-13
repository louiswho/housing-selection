using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class TestRoomRepository
    {

        public readonly IDbContext mockHousingContext;

        #region Constructor
        /// <summary>
        /// create a mock dbContext object and setup relevent methods and properties
        /// </summary>
        public TestRoomRepository()
        {
            //Use var instad of Mock<IDbContext>
            Mock<IDbContext> mockHousingContext = new Mock<IDbContext>();
            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C"); //Guid.NewGuid
            var guid1 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1D"); //Guid.NewGuid
            var guid2 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1E");//Guid.NewGuid
            var guid3 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1F");//Guid.NewGuid
            Address address = new Address(); //use var
            Room testRoom1 = new Room()
            {
                Id = guid,
                RoomId = guid1,
                Gender = "m",
                Location = "Reston",
                Occupancy = 3,
                Vacancy = 2,
                Address = address


            };
            Room testRoom2 = new Room()
            {
                Id = guid2,
                RoomId = guid3,
                Gender = "f",
                Location = "Reston",
                Occupancy = 2,
                Vacancy = 2,
                Address = address



            };
            List<Room> RoomList = new List<Room>
        {
            testRoom1,
            testRoom2
        };

            DbSet<Room> myDbSet = TestingUtilities.GetQueryableMockDbSet(RoomList);
            //Remove old comments
            mockHousingContext.Setup(x => x.Rooms).Returns(myDbSet);
            //mockHousingContext.Setup(x => x.Rooms.Add(It.IsAny<Room>()));
            //.Callback((Room item) => itemsInserted.Add(item));
            this.mockHousingContext = mockHousingContext.Object;
        }
        #endregion
        //Remove all regions
        [Fact]
        public void CanReturnRooms()
        {

            //Remove unnecessary comments
            RoomRepository roomRepository = new RoomRepository(mockHousingContext);
            // Try finding all batches

            var testRooms = roomRepository.GetRooms();

            Assert.NotNull(testRooms); // Test if null
        }

        [Fact]
        public void CanReturnRoomsById()
        {
            RoomRepository roomRepository = new RoomRepository(mockHousingContext);
            //Use var
            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C"); //Guid.NewGuid
            var guid1 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1D"); //Guid.NewGuid
            // Try finding a room by id
            var testRoom = roomRepository.GetRoomById(guid);
            Assert.Equal(guid1, testRoom.RoomId); // Verify it is the right room.
        }

        [Fact]
        public void CanSaveChanges()
        {


            //Use var
            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();
            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);
            //Use var
            RoomRepository roomRepository = new RoomRepository(MockHousingContext.Object);
            roomRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());


        }

        [Fact]
        public void CanAddRoom()
        {
            
            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();
            

            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1E"); //Guid.NewGuid
            Address address = new Address();
            Room testRoom1 = new Room()
            {
                Id = guid,
                RoomId = guid,
                Gender = "m",
                Location = "Reston",
                Occupancy = 3,
                Vacancy = 2,
                Address = address
            };
            List<Room> roomList = new List<Room>()
            {
                  testRoom1
            };
            DbSet<Room> myDbSet = TestingUtilities.GetQueryableMockDbSet(roomList);

            MockHousingContext.Setup(x => x.Rooms).Returns(myDbSet);

            MockHousingContext.Setup(x => x.Rooms.Add(It.IsAny<Room>()));
   

            RoomRepository roomRepository = new RoomRepository(MockHousingContext.Object);       

           roomRepository.AddRoom(testRoom1);

            MockHousingContext.Verify(m => m.Rooms.Add(It.IsAny<Room>()), Times.Once());

        }
    }
}
