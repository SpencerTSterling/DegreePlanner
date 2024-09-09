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

        public IActionResult Index()
        {
            // Get logged in user's ID
            var userId = _userManager.GetUserId(User);

            // Fetch terms only assocaited with the logged in user and order by start date 
            var terms = _uow.Term.GetAll(u => u.UserId == userId).OrderBy(t => t.StartDate);
            var viewModel = new DashboardVM
            {
                TermsList = terms.ToList(),
                
            };
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

    }
}
