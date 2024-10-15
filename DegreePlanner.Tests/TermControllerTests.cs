using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;// creating test subs (mocks)
using Xunit;
using DegreePlannerWeb;
using DegreePlanner.Controllers;
using DegreePlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore;
using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using DegreePlanner.Models.ViewModels;

namespace DegreePlanner.Tests
{
    public class TermControllerTests
    {

        // Mocking the required dependencies
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly TermController _controller;

        public TermControllerTests()
        {
            // Initialize the mocks
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Mocking UserManager requires a bit more setup (explained below)
            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null
            );

            // Create an instance of the TermController, passing in the mocked dependencies
            _controller = new TermController(_mockUnitOfWork.Object, _mockUserManager.Object);
        }


        [Fact]
        public async Task Upsert_Post_ReturnsRedirectToActionResult_WhenTermIsValid()
        {
            // Arrange: Set up mock data
            var userId = "test-user-id";
            var user = new IdentityUser { Id = userId };

            // Set up the mock to return a valid user ID
            _mockUserManager.Setup(um => um.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns(userId);
            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).ReturnsAsync(user);

            // Create a valid Term ViewModel
            var termVM = new TermVM
            {
                Term = new Term
                {
                    Id = 0, // This simulates a new term being created
                    Name = "Spring 2024",
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 5, 1)
                }
            };

            // Act: Call the Upsert method with valid data
            var result = await _controller.UpsertAsync(termVM);

            // Assert: Verify the expected behavior
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); // Expect a redirect
            Assert.Equal("Index", redirectResult.ActionName); // Redirects to "Index" action in DegreePlan

            // Verify that Add and Save were called
            _mockUnitOfWork.Verify(uow => uow.Term.Add(It.IsAny<Term>()), Times.Once); // Ensure Add was called
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once); // Ensure Save was called
        }
    }

}
