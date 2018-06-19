using System;
using System.Collections.Generic;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context.DataAccess
{
    public class TestUserRepository
    {
        private readonly IDbContext _mockHousingContext;
        private readonly List<User> _userList = new List<User>();
        private readonly User _testUser1 = new User();
        private readonly User _testUser2 = new User();
        private readonly Guid _guid;
        private readonly Guid _guid1;

        public TestUserRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            _guid = Guid.NewGuid();
            _guid1 = Guid.NewGuid();

            _testUser1.Id = _guid;
            _testUser1.UserId = _guid1;

            _testUser2.Id = _guid1;
            _testUser2.UserId = _guid;

            _userList.Add(_testUser1);
            _userList.Add(_testUser2);

            var myDbSet = TestingUtilities.GetQueryableMockDbSet(_userList);

            mockHousingContext.Setup(x => x.Users).Returns(myDbSet);

            _mockHousingContext = mockHousingContext.Object;
        }
        
        [Fact]
        public void CanGetUsers()
        {
            var userRepository = new UserRepository(_mockHousingContext);

            var users = userRepository.GetUsers();

            Assert.NotNull(users);
        }

        [Fact]
        public async void CanGetUsersByIdAsync()
        {
            var userRepository = new UserRepository(_mockHousingContext);

            var user =  await userRepository.GetUserById(_guid);

            Assert.Equal(_guid1, user.UserId);
        }

        [Fact]
        public void CanAddUser()
        {
            var mockDbContext = new Mock<IDbContext>();

            var myDbSet = TestingUtilities.GetQueryableMockDbSet(_userList);

            mockDbContext.Setup(x => x.Users).Returns(myDbSet);

            mockDbContext.Setup(x => x.Users.Add(It.IsAny<User>()));

            var userRepository = new UserRepository(mockDbContext.Object);

            userRepository.AddUser(_testUser1);

            mockDbContext.Verify(x => x.Users.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void CanSaveChangesAsync()
        {
            var mockHousingContext = new Mock<IDbContext>();

            mockHousingContext.Setup(x => x.SaveChanges()).Returns(1);

            var userRepository = new UserRepository(mockHousingContext.Object);

            await userRepository.SaveChangesAsync();

            mockHousingContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
