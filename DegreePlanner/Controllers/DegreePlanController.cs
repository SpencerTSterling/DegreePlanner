using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DegreePlannerWeb.Controllers
{
    [Authorize] // Ensure only logged-in users can access these actions
    public class DegreePlanController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager; // inject UserManager

        public DegreePlanController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _uow = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Get logged in user's ID
            var userId = _userManager.GetUserId(User);
            // Get all the terms belonging to the logged-in user, ordered by their start date
            var terms = _uow.Term.GetAll(t => t.UserId == userId).OrderBy(t => t.StartDate);

            // Create a list of TermsWithCourses view model 
            var termsWithCourses = terms.Select(term => new TermWithCourses
            {
                Term = term,
                // Get all courses in the current term ordered by their start date
                Courses = _uow.Course.GetAll(c => c.TermId == term.Id).OrderBy(c => c.StartDate)
            }).ToList();

            // Create the DegreePlanVM and populate it with the terms and courses
            var viewModel = new DegreePlanVM
            {
                TermsWithCourses = termsWithCourses
            };

            return View(viewModel);
        }

        // Action to fetch and return the details of a course
        [HttpGet]
        public IActionResult CourseDetails(int id)
        {
            // Fetch the course by its ID
            var course = _uow.Course.Get(c => c.Id == id);
            if (course == null)
            {
                // Return a 404 Not Found result if the course doesn't exist
                return NotFound();
            }
            // Return a partial view with the course details
            return PartialView("_CourseDetails", course);
        }
    }
}
