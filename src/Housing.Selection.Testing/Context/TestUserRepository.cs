using System;
using System.Collections.Generic;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class TestUserRepository
    {
        private readonly IDbContext mockHousingContext;
        private List<User> userList = new List<User>();
        private User testUser1 = new User();
        private User testUser2 = new User();
        private Guid guid;
        private Guid guid1;

        public TestUserRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            guid = Guid.NewGuid();
            guid1 = Guid.NewGuid();

            testUser1.Id = guid;
            testUser1.UserId = guid1;

            testUser2.Id = guid1;
            testUser2.UserId = guid;

            userList.Add(testUser1);
            userList.Add(testUser2);

            DbSet<User> myDbSet = TestingUtilities.GetQueryableMockDbSet(userList);

            mockHousingContext.Setup(x => x.Users).Returns(myDbSet);

            this.mockHousingContext = mockHousingContext.Object;

        }
        
        [Fact]
        public void CanGetUsers()
        {
            var userRepository = new UserRepository(mockHousingContext);

            var users = userRepository.GetUsers();

            Assert.NotNull(users);
        }

        [Fact]
        public void CanGetUsersById()
        {
            UserRepository userRepository = new UserRepository(mockHousingContext);

            var user = userRepository.GetUserById(guid);

            Assert.Equal(guid1, user.UserId);
        }

        [Fact]
        public void CanAddUser()
        {
            var MockDbContext = new Mock<IDbContext>();

            DbSet<User> myDbSet = TestingUtilities.GetQueryableMockDbSet(userList);

            MockDbContext.Setup(x => x.Users).Returns(myDbSet);

            MockDbContext.Setup(x => x.Users.Add(It.IsAny<User>()));

            var userRepository = new UserRepository(MockDbContext.Object);

            userRepository.AddUser(testUser1);

            MockDbContext.Verify(x => x.Users.Add(It.IsAny<User>()), Times.Once);

        }

        [Fact]
        public void CanSaveChanges()
        {
            var MockHousingContext = new Mock<IDbContext>();

            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);

            var userRepository = new UserRepository(MockHousingContext.Object);

            userRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());

        }
    }
}
