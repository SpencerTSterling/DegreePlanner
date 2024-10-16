using Xunit;
using Moq;
using DegreePlanner.Controllers;
using DegreePlanner.Models;
using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models.ViewModels;
using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using DegreePlannerWeb.Controllers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

public class UnitTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
    private readonly CourseController _controller;

    public UnitTests()
    {
        // Mock the UnitOfWork and UserManager
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _mockUserManager = MockUserManager();
        _controller = new CourseController(_mockUnitOfWork.Object, _mockUserManager.Object);
    }

    private Mock<UserManager<IdentityUser>> MockUserManager()
    {
        var store = new Mock<IUserStore<IdentityUser>>();
        return new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
    }

    /// <summary>
    /// Verifies if the Index action of the CourseController returns the correct view with a list of Courses.
    /// </summary>
    [Fact]
    public void CourseIndex_ReturnsViewResult()
    {
        // Arrange
        var courses = new List<Course>
        {
            new Course { Name = "Math 101" },
            new Course { Name = "History 101" }
        };

        var mockUser = new IdentityUser { UserName = "testuser", Id = "123" };
        // Set up the mock UserManager to return the mock user
        _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(mockUser);

        // Set up the mock UnitOfWork to return the courses
        _mockUnitOfWork.Setup(uow => uow.Course.GetAll(
            It.IsAny<System.Linq.Expressions.Expression<Func<Course, bool>>>(), // mocks any filter expression that may be passed
            It.IsAny<string>())) // handles the optional 'includeProperties' argument
            .Returns(courses);

        // Act
        var result = _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult.Model); // Check if the model passed to the view is not null
        var model = Assert.IsAssignableFrom<IEnumerable<Course>>(viewResult.Model);
        Assert.Equal(2, model.Count()); // Ensure correct number of courses
    }


}
