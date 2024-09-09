using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegreePlannerWeb.Controllers
{
    [Authorize] // Ensure only logged-in users can access these actions
    public class CourseItemController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager; // Inject UserManager

        public CourseItemController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _uow = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Get logged in user's ID
            var userId = _userManager.GetUserId(User);

            // Fetch course items only associated with the logged in user
            List<CourseItem> objCourseItemList = _uow.CourseItem.GetAll(c => c.Course.Term.UserId == userId, includeProperties: "Course").ToList();

            return View(objCourseItemList);
        }

        #region Upsert (Update/Insert)
        public IActionResult Upsert(int? id)
        {
            var userId = _userManager.GetUserId(User);

            CourseItemVM courseitemVM = new()
            {
                // Filter course belonging to the logged-in user
                CourseList = _uow.Course.GetAll(c => c.Term.UserId == userId).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CourseItem = new CourseItem()
            };
            if (id == null || id == 0) // if ID doesn't exist
            {
                //Create 
                return View(courseitemVM);
            }
            else
            {
                //Update
                courseitemVM.CourseItem = _uow.CourseItem.Get(u => u.Id == id && u.Course.Term.UserId == userId);
                if (courseitemVM.CourseItem == null)
                {
                    return Forbid(); // Prevent users from editing other's course items
                }
                return View(courseitemVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(CourseItemVM courseitemVM)
        {
            var userId = _userManager.GetUserId(User);

            // Get the course selection (ensure it belongs to the logged in user)
            Course selectedCourse = _uow.Course.Get(t => t.Id == courseitemVM.CourseItem.CourseId && t.Term.UserId == userId);

            if (ModelState.IsValid)
            {
                // Assign the selected Course to the CourseItem
                courseitemVM.CourseItem.Course = selectedCourse;

                if (courseitemVM.CourseItem.Id == 0)
                {
                    // add a new item
                    _uow.CourseItem.Add(courseitemVM.CourseItem);
                }
                else
                {
                    // Ensure the user is not updating a course they don't own
                    var existingCourseItem = _uow.CourseItem.Get(c => c.Id == courseitemVM.CourseItem.Id);

                    if (existingCourseItem == null)
                    {
                        return Forbid();
                    }

                    existingCourseItem.Course = courseitemVM.CourseItem.Course;

                    // update an existing item
                    _uow.CourseItem.Update(courseitemVM.CourseItem);
                }
            }

            _uow.Save();
            //Return to list page
            return RedirectToAction("Index", "CourseItem");

        }

        #endregion

        #region Delete

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CourseItem? selectedItem = _uow.CourseItem.Get(u=> u.Id == id);
            if (selectedItem == null)
            {
                return NotFound();
            }
            return View(selectedItem);
        }

        // Deleting a CourseItem
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            CourseItem? obj = _uow.CourseItem.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }
            _uow.CourseItem.Delete(obj);
            _uow.Save();
            // Success notification
            TempData["success"] = "Course deleted successfully";
            //Redirect to index
            return RedirectToAction("Index", "CourseItem");
        }


        #endregion

    }
}
