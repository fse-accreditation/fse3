using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using user.Models;
using user.Repository;
using user.Services;
using Test.UserConstraints;
using user.Exceptions;

namespace Test
{
    [TestFixture]
    public class UserServiceTest
    {
        private static List<UserDetails> users;

        [OneTimeSetUp]
        public void Setup()
        {
            this.SeedData();
        }

        [Test]
        [Order(1)]
        public void ShouldCreateUserSuccess()
        {
            var user = new UserDetails
            {
                FirstName = "Riddhi",
                LastName = "RoyChowdhuri",
                Address = "Technopolis",
                City = "Kolkata",
                Phone = "9945467890",
                Password = "Riddhi@123",
                Email = "Riddhi.RoyChowdhuri@cognizant.com",
                Pin = "700001",
                State = "WB",
                UserType = "Seller"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(m => m.AddUser(It.IsAny<UserDetails>()));

            var service = new UserService(mockUserRepository.Object);

            service.Register(user);
            mockUserRepository.Verify(m => m.AddUser(It.IsAny<UserDetails>()), Times.Once);
        }

        [Test]
        [Order(2)]
        public void ShouldAddUserThrowException()
        {
            var user = new UserDetails
            {
                FirstName = "Riddhi",
                LastName = "RoyChowdhuri",
                Address = "Technopolis",
                City = "Kolkata",
                Phone = "9945467890",
                Password = "Riddhi@123",
                Email = "Riddhi.RoyChowdhuri@cognizant.com",
                Pin = "700001",
                State = "WB",
                UserType = "Seller"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(m => m.GetUserByMail(It.IsAny<string>())).Returns(user);
            mockUserRepository.Setup(m => m.AddUser(It.IsAny<UserDetails>()));

            var service = new UserService(mockUserRepository.Object);

            Assert.That(
                () => service.AddUser(user),
                Throws.TypeOf<UserAlreadyExistsException>().And.Message.EqualTo("User with given email exists").IgnoreCase);
            mockUserRepository.Verify(m => m.AddUser(It.IsAny<UserDetails>()), Times.Never);
            mockUserRepository.Verify(m => m.GetUserByMail(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Order(2)]
        public void ShouldAddUserSuccess()
        {
            var user = new UserDetails
            {
                FirstName = "Riddhi",
                LastName = "RoyChowdhuri",
                Address = "Technopolis",
                City = "Kolkata",
                Phone = "9945467890",
                Password = "Riddhi@123",
                Email = "Riddhi.RoyChowdhuri01@cognizant.com",
                Pin = "700001",
                State = "WB",
                UserType = "Buyer"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(m => m.GetUserByMail(It.IsAny<string>())).Returns((UserDetails)null);
            mockUserRepository.Setup(m => m.AddUser(It.IsAny<UserDetails>()));

            var service = new UserService(mockUserRepository.Object);

            service.AddUser(user);
            mockUserRepository.Verify(m => m.AddUser(It.IsAny<UserDetails>()), Times.Once);
            mockUserRepository.Verify(m => m.GetUserByMail(It.IsAny<string>()), Times.Once);
        }
        [Test]
        [Order(3)]
        public void ShouldLoginWithValidUserSuccess()
        {
            var user = new UserDetails
            {
                FirstName = "Riddhi",
                LastName = "RoyChowdhuri",
                Address = "Technopolis",
                City = "Kolkata",
                Phone = "9945467890",
                Password = "Riddhi@123",
                Email = "Riddhi.RoyChowdhuri01@cognizant.com",
                Pin = "700001",
                State = "WB",
                UserType = "Buyer"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(m => m.GetUserByCredential(It.IsAny<string>(),It.IsAny<string>())).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            var result = service.Login(user.Email,user.Password);
            mockUserRepository.Verify(m => m.GetUserByCredential(It.IsAny<string>(),It.IsAny<string>()), Times.Once);
            Assert.That(result, NUnit.Framework.Is.Not.Null);
            Assert.That(result, NUnit.Framework.Is.AssignableFrom<UserDetails>());
            Assert.That(result, UserConstraints.Is.IdenticalTo(user));
        }

        [Test]
        [Order(4)]
        public void ShouldLoginWithInValidUserThrowException()
        {
            var user = new UserDetails
            {
                FirstName = "Riddhi",
                LastName = "RoyChowdhuri",
                Address = "Technopolis",
                City = "Kolkata",
                Phone = "9945467890",
                Password = "Riddhi@123",
                Email = "Riddhi.RoyChowdhuri01@cognizant.com",
                Pin = "700001",
                State = "WB",
                UserType = "Buyer"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(m => m.GetUserByCredential(It.IsAny<string>(), It.IsAny<string>())).Returns((UserDetails)null);

            var service = new UserService(mockUserRepository.Object);
            Assert.That(
               () => service.Login(user.Email, user.Password),
               Throws.TypeOf<UserNotFoundException>()
                           .And.Message.EqualTo("Invalid credentials").IgnoreCase);
            mockUserRepository.Verify(m => m.GetUserByCredential(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockUserRepository.Verify(m => m.AddUser(It.IsAny<UserDetails>()), Times.Never);
        }

        private void SeedData()
        {
            users = new List<UserDetails>()
            {
                 new UserDetails
            {
                FirstName = "Sukanya",
                LastName = "Das",
                Address = "Santragachi",
                City = "Howrah",
                Phone = "9937438500",
                Password = "Sukanya@123",
                Email = "Sukanya.Das@cognizant.com",
                Pin = "711104",
                State = "WB",
                UserType = "Seller"
            },
            new UserDetails
            {
                FirstName = "Subha",
                LastName = "Kumar",
                Address = "Technopolis",
                City = "Kolkata",
                Phone = "99434567890",
                Password = "Subha@123",
                Email = "Subha.Kumar@cognizant.com",
                Pin = "700001",
                State = "WB",
                UserType = "Buyer"
            }

            };

        }

    }
}
