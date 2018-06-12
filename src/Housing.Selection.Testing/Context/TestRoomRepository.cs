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


        public TestRoomRepository()
        {

            Mock<IDbContext> mockHousingContext = new Mock<IDbContext>();
            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C");
            var guid1 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1D");
            var guid2 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1E");
            var guid3 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1F");
            Address address = new Address();
            Room testRoom1 = new Room()
            {
                Id = guid,
                RoomId = guid1,
                Gender = 'm',
                Location = "Reston",
                Occupancy = 3,
                Vacancy = 2,
                Address = address


            };
            Room testRoom2 = new Room()
            {
                Id = guid2,
                RoomId = guid3,
                Gender = 'f',
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

            mockHousingContext.Setup(x => x.Rooms).Returns(myDbSet);
            mockHousingContext.Setup(x => x.Rooms.Add(It.IsAny<Room>()));
            //.Callback((Room item) => itemsInserted.Add(item));
            this.mockHousingContext = mockHousingContext.Object;
        }

        [Fact]
        public void CanReturnBatches()
        {


            RoomRepository roomRepository = new RoomRepository(mockHousingContext);
            // Try finding all batches

            var testRooms = roomRepository.GetRooms();

            Assert.NotNull(testRooms); // Test if null
        }

        [Fact]
        public void CanReturnRoomsById()
        {
            RoomRepository roomRepository = new RoomRepository(mockHousingContext);

            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C");
            var guid1 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1D");
            // Try finding a room by id
            var testRoom = roomRepository.GetRoomById(guid);
            Assert.Equal(guid1, testRoom.RoomId); // Verify it is the right room.
        }

        [Fact]
        public void CanSaveChanges()
        {
            
            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();
            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);

            RoomRepository roomRepository = new RoomRepository(MockHousingContext.Object);
            roomRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());


        }

        [Fact]
        public void CanAddRoom()
        {
            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();
            

            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1E");
            Address address = new Address();
            Room testRoom1 = new Room()
            {
                Id = guid,
                RoomId = guid,
                Gender = 'm',
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
