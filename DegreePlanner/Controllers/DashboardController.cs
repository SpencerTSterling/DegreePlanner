using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DegreePlannerWeb.Controllers
{
    [Authorize] // Ensure only logged-in users can access these actions
    public class DashboardController : Controller
    {

        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager; // Inject UserManager


        public DashboardController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _uow = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index(int? selectedTermId)
        {
            // Get logged in user's ID
            var userId = _userManager.GetUserId(User);

            // Fetch terms only assocaited with the logged in user and order by start date 
            var terms = _uow.Term.GetAll(u => u.UserId == userId).OrderBy(t => t.StartDate);
            var viewModel = new DashboardVM
            {
                TermsList = terms.ToList(),
            };
            if (selectedTermId != null)
            {
                viewModel.SelectedTermId = selectedTermId;
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetCoursesByTerm(int termId)
        {
            var userId = _userManager.GetUserId(User);
            // fetch all courses that belong to the Term, where the term belongs to the logged in user
            var coursesList = _uow.Course.GetAll (c => c.TermId == termId && c.Term.UserId == userId);
            return PartialView("_CourseCards", coursesList);

        }

        [HttpGet]
        public IActionResult GetCourseItemsByTerm(int termId)
        {
            var userId = _userManager.GetUserId(User);

            // List of courses in the Term
            var coursesList = _uow.Course.GetAll(c => c.TermId == termId  && c.Term.UserId == userId);
            // list of the CourseIds
            var courseIds = coursesList.Select(c => c.Id).ToList();
            // Get list of course items in the courses
            var courseItemList = _uow.CourseItem.GetAll(ci => courseIds.Contains(ci.CourseId))
                    .OrderBy(ci => ci.DueDate ?? DateTime.MaxValue)
                    .ToList();

            return PartialView("_CourseItemsList", courseItemList);
        }

        [HttpPost]
        public IActionResult MarkCourseItemAsComplete(int id, bool isCompleted)
        {
            var userId = _userManager.GetUserId(User);

            // Fetch the course item by ID, ensuring the item belongs to the logged-in user
            var courseItem = _uow.CourseItem.Get(ci => ci.Id == id && ci.Course.Term.UserId == userId);
            if (courseItem == null)
            {
                return NotFound();
            }

            // Update the course item's IsCompleted field
            courseItem.IsCompleted = isCompleted;
            _uow.Save(); // Save the changes to the database

            return Ok();
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            var userId = _userManager.GetUserId(User);

            // Fetch matching terms, courses, and course items
            var terms = _uow.Term.GetAll(t => t.UserId == userId && t.Name.Contains(query)).ToList();
            var courses = _uow.Course.GetAll(c => c.Term.UserId == userId && c.Name.Contains(query)).ToList();
            var courseItems = _uow.CourseItem.GetAll(ci => ci.Course.Term.UserId == userId && ci.Name.Contains(query)).ToList();

            var searchResults = new
            {
                Terms = terms.Select(t => new { t.Id, t.Name }),
                Courses = courses.Select(c => new { c.Id, c.Name }),
                CourseItems = courseItems.Select(ci => new { ci.Id, ci.Name, ci.CourseId })
            };

            return Json(searchResults);
        }


    }
}
