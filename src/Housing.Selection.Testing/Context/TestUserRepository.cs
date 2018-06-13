using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class TestUserRepository
    {
        //Clean up white space, remove uncessary namespaces. 
        private readonly IDbContext mockHousingContext;
        private List<User> userList = new List<User>();

        public TestUserRepository()
        {
            Mock<IDbContext> mockHousingContext = new Mock<IDbContext>();

            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1E"); // Guid.NewGuid()
            var guid1 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1F"); // Guid.NewGuid()
            var guid2 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C"); // Guid.NewGuid()
            var guid3 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1A"); // Guid.NewGuid()
            User testUser1 = new User() {
                Id = guid,
                UserId = guid1

            };
            User testUser2 = new User()
            {
                Id = guid2,
                UserId = guid3

            };

         
            userList.Add(testUser1);
            userList.Add(testUser2);


            DbSet<User> myDbSet = TestingUtilities.GetQueryableMockDbSet(userList);
            mockHousingContext.Setup(x => x.Users).Returns(myDbSet);
            this.mockHousingContext = mockHousingContext.Object;

        }

        [Fact]
        public void CanGetUsers(){
            //Fix bracket and clean up alignment
         UserRepository userRepository = new UserRepository(mockHousingContext);

          var users = userRepository.GetUsers();

            Assert.NotNull(users);

          }
        [Fact]
        public void CanGetUsersById()
        {
            UserRepository userRepository = new UserRepository(mockHousingContext);

            // matches testuser1 id
            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1E"); // Guid.NewGuid()
            //matches testuser1 userId
            var guid1 = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1F"); // Guid.NewGuid()

            var user = userRepository.GetUserById(guid);

            Assert.Equal(guid1, user.UserId);
            //Clean up white space.

        }





        [Fact]
        public void CanAddUser()
        {
            Mock<IDbContext> MockDbContext = new Mock<IDbContext>();
            //Use var
            DbSet<User> myDbSet = TestingUtilities.GetQueryableMockDbSet(userList);
            MockDbContext.Setup(x => x.Users).Returns(myDbSet);
            MockDbContext.Setup(x => x.Users.Add(It.IsAny<User>()));
            UserRepository userRepository = new UserRepository(MockDbContext.Object);

            var guid = Guid.NewGuid();
       
            User testUser1 = new User()
            {
                Id = guid,
                UserId = guid

            };

            userRepository.AddUser(testUser1);

            MockDbContext.Verify(x => x.Users.Add(It.IsAny<User> ()), Times.Once );

        }

        [Fact]
        public void CanSaveChanges()
        {



            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();
            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);

            UserRepository userRepository = new UserRepository(MockHousingContext.Object);
            userRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());


        }



        //Fix white space and allignement.

    }
        


    }
