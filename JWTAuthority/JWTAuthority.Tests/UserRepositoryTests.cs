using JWTAuthority.DataAccess;
using JWTAuthority.DataAccess.Repository;
using JWTAuthority.Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace JWTAuthority.Tests
{
    [TestFixture]
    class UserRepositoryTests
    {
        private UserRepository _userRepostiory;
        private JWTContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<JWTContext>()
                .UseInMemoryDatabase("InMemoryDB")
                .Options;

            _context = new JWTContext(options);
            _userRepostiory = new UserRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void AddUser_WhenCalled_UserIsAddedToTheDatabase()
        {
            //Arrange
            var testUser = GetTestUser();

            //Act
            _userRepostiory.AddUser(testUser);

            //Assert
            Assert.IsTrue(_context.Users.Count() == 1);
            Assert.IsTrue(_context.Users.Where(user => user.Username == testUser.Username) != null);
        }

        [Test]
        public void GetByUsername_WhenCalledAndUserExists_CorrectUserIsReturned()
        {
            //Arrange
            var testUsers = new List<User>
            {
                new User { Username = "test1", Password = "test1", Email = "test1@gmail.com" },
                new User { Username = "test2", Password = "test2", Email = "test2@gmail.com" },
                new User { Username = "test3", Password = "test3", Email = "test3@gmail.com" }
            };

            _context.AddRange(testUsers);
            _context.SaveChanges();

            //Act
            var resultUser = _userRepostiory.GetByUsername("test2");

            //Assert
            Assert.IsTrue(resultUser != null);
            Assert.AreEqual(testUsers[1] , resultUser);
        }


        [Test]
        public void GetByUsername_WhenCalledAndUserDoesNotExist_NullIsReturned()
        {
            //Arrange
            var testUsers = new List<User>
            {
                new User { Username = "test1", Password = "test1", Email = "test1@gmail.com" },
                new User { Username = "test2", Password = "test2", Email = "test2@gmail.com" },
                new User { Username = "test3", Password = "test3", Email = "test3@gmail.com" }
            };

            _context.AddRange(testUsers);
            _context.SaveChanges();

            //Act
            var resultUser = _userRepostiory.GetByUsername("test10");

            //Assert
            Assert.IsTrue(resultUser == null);
        }


        [Test]
        public void IsAvaibleUsername_WhenCalledAndUserIsAvaible_CorrectResultIsReturned()
        {
            //Arrange
            var testUsers = new List<User>
            {
                new User { Username = "test1", Password = "test1", Email = "test1@gmail.com" },
                new User { Username = "test2", Password = "test2", Email = "test2@gmail.com" },
                new User { Username = "test3", Password = "test3", Email = "test3@gmail.com" }
            };

            _context.AddRange(testUsers);
            _context.SaveChanges();

            //Act
            var result = _userRepostiory.IsAvailableUsername("test10");

            //Arrange
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAvaibleUsername_WhenCalledAndUserIsNotAvaible_CorrectResultIsReturned()
        {
            //Arrange
            var testUsers = new List<User>
            {
                new User { Username = "test1", Password = "test1", Email = "test1@gmail.com" },
                new User { Username = "test2", Password = "test2", Email = "test2@gmail.com" },
                new User { Username = "test3", Password = "test3", Email = "test3@gmail.com" }
            };

            _context.AddRange(testUsers);
            _context.SaveChanges();

            //Act
            var result = _userRepostiory.IsAvailableUsername("test1");

            //Arrange
            Assert.IsFalse(result);
        }

        private User GetTestUser()
        {
            return new User
            {
                Username = "test",
                Password = "test",
                Email = "test@gmail.com"
            };
        }
    }
}
