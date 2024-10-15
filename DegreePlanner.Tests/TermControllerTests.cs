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

namespace DegreePlanner.Tests
{
    internal class TermControllerTests
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

    }
}
