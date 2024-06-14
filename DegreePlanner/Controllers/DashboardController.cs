using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DegreePlannerWeb.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IUnitOfWork _uow;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public IActionResult Index()
        {
            var terms = _uow.Term.GetAll().OrderBy(t => t.StartDate);
            var viewModel = new DashboardVM
            {
                TermsList = terms.ToList(),
                
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetCoursesByTerm(int termId)
        {
            var coursesList = _uow.Course.GetAll (c => c.TermId == termId);
            return PartialView("_CourseCards", coursesList);
        }

        [HttpGet]
        public IActionResult GetCourseItemsByTerm(int termId)
        {
            // List of courses in the Term
            var coursesList = _uow.Course.GetAll(c => c.TermId == termId);
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
