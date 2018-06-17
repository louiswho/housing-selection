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

        private readonly IDbContext mockHousingContext;
        private List<Room> roomList = new List<Room>();
        private Room testRoom1 = new Room();
        private Room testRoom2 = new Room();
        private Guid guid;
        private Guid guid1;

        
        public TestRoomRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            guid =  Guid.NewGuid();
            guid1 = Guid.NewGuid();

            testRoom1.Id = guid;
            testRoom1.RoomId = guid1;

            testRoom2.Id = guid1;
            testRoom2.RoomId = guid;

            roomList.Add(testRoom1);
            roomList.Add(testRoom2);

            DbSet<Room> myDbSet = TestingUtilities.GetQueryableMockDbSet(roomList);

            mockHousingContext.Setup(x => x.Rooms).Returns(myDbSet);
         
            this.mockHousingContext = mockHousingContext.Object;
        }


        [Fact]
        public void CanReturnRooms()
        {
            var roomRepository = new RoomRepository(mockHousingContext);

            var testRooms = roomRepository.GetRooms();

            Assert.NotNull(testRooms); 
        }

        [Fact]
        public void CanReturnRoomsById()
        {
            var roomRepository = new RoomRepository(mockHousingContext);

            var testRoom = roomRepository.GetRoomById(guid);

            Assert.Equal(guid1, testRoom.RoomId); 
        }

        [Fact]
        public void CanSaveChanges()
        {           
            var MockHousingContext = new Mock<IDbContext>();

            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);

            var  roomRepository = new RoomRepository(MockHousingContext.Object);

            roomRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());

        }

        [Fact]
        public void CanAddRoom()
        {           
            var MockHousingContext = new Mock<IDbContext>();
            
            DbSet<Room> myDbSet = TestingUtilities.GetQueryableMockDbSet(roomList);

            MockHousingContext.Setup(x => x.Rooms).Returns(myDbSet);

            MockHousingContext.Setup(x => x.Rooms.Add(It.IsAny<Room>()));
   
            var roomRepository = new RoomRepository(MockHousingContext.Object);       

            roomRepository.AddRoom(testRoom1);

            MockHousingContext.Verify(m => m.Rooms.Add(It.IsAny<Room>()), Times.Once());

        }
    }
}
