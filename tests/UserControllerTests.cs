using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private static List<User> userList;
        private UserController controller;

        [TestInitialize]
        public void SetUp()
        {
            // Initialize your user list and controller here
            userList = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };

            UserController.userlist = userList; // Assuming userlist is public for simplicity
            controller = new UserController();
        }

        [TestMethod]
        public void Index_ReturnsViewWithUsers()
        {
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<User>;
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void Create_PostAddsUserAndRedirects()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "New User", Email = "newuser@example.com" };

            // Act
            var result = controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(3, UserController.userlist.Count); // Verify the user was added
        }
    }
}
