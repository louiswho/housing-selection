using System;
using System.Collections.Generic;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context.DataAccess
{
    public class TestRoomRepository
    {
        private readonly IDbContext _mockHousingContext;
        private readonly List<Room> _roomList = new List<Room>();
        private readonly Room _testRoom1 = new Room();
        private readonly Room _testRoom2 = new Room();
        private readonly Guid _guid;
        private readonly Guid _guid1;

        public TestRoomRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            _guid = Guid.NewGuid();
            _guid1 = Guid.NewGuid();

            _testRoom1.Id = _guid;
            _testRoom1.RoomId = _guid1;

            _testRoom2.Id = _guid1;
            _testRoom2.RoomId = _guid;

            _roomList.Add(_testRoom1);
            _roomList.Add(_testRoom2);

            var myDbSet = TestingUtilities.GetQueryableMockDbSet(_roomList);

            mockHousingContext.Setup(x => x.Rooms).Returns(myDbSet);

            _mockHousingContext = mockHousingContext.Object;
        }


        [Fact]
        public void CanReturnRooms()
        {
            var roomRepository = new RoomRepository(_mockHousingContext);

            var testRooms = roomRepository.GetRooms();

            Assert.NotNull(testRooms);
        }

        [Fact]
        public async void CanReturnRoomsByIdAsync()
        {
            var roomRepository = new RoomRepository(_mockHousingContext);

            var testRoom = await roomRepository.GetRoomById(_guid);

            Assert.Equal(_guid1, testRoom.RoomId);
        }

        [Fact]
        public async void CanSaveChangesAsync()
        {
            var mockHousingContext = new Mock<IDbContext>();

            mockHousingContext.Setup(x => x.SaveChanges()).Returns(1);

            var roomRepository = new RoomRepository(mockHousingContext.Object);

            await roomRepository.SaveChangesAsync();

            mockHousingContext.Verify(m => m.SaveChanges(), Times.Once());

        }

        [Fact]
        public void CanAddRoom()
        {
            var mockHousingContext = new Mock<IDbContext>();

            var myDbSet = TestingUtilities.GetQueryableMockDbSet(_roomList);

            mockHousingContext.Setup(x => x.Rooms).Returns(myDbSet);

            mockHousingContext.Setup(x => x.Rooms.Add(It.IsAny<Room>()));

            var roomRepository = new RoomRepository(mockHousingContext.Object);

            roomRepository.AddRoom(_testRoom1);

            mockHousingContext.Verify(m => m.Rooms.Add(It.IsAny<Room>()), Times.Once());
        }
    }
}
