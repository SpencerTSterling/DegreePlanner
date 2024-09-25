using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository;
using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DegreePlanner.Controllers
{
    [Authorize] // Ensure only logged-in users can access these actions
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _uow; //unit of work
        private readonly UserManager<IdentityUser> _userManager; // Inject UserManager


        public CourseController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        { 
            _uow = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Get logged in user's ID
            var userId = _userManager.GetUserId(User);

            // Fetch courses only associated with the logged in user
            List<Course> objCourseList = _uow.Course.GetAll(c => c.Term.UserId == userId, includeProperties: "Term").ToList();
            return View(objCourseList);
        }


        #region Upsert (Update/Insert)
        public IActionResult Upsert(int? id)
        {
            var userId = _userManager.GetUserId(User);


            CourseVM courseVM = new()
            {
                // Filter terms belonging to the logged-in user
                TermList = _uow.Term.GetAll(t => t.UserId == userId).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Course = new Course()
            };
            if (id == null || id == 0) // if ID doesn't exist
            {

                // Initialize default values for StartDate and EndDate to today's date
                courseVM.Course.StartDate = DateTime.Today;
                courseVM.Course.EndDate = DateTime.Today;

                // Create
                return View(courseVM);
            }
            else
            {
                // Update
                courseVM.Course = _uow.Course.Get(u => u.Id == id && u.Term.UserId == userId);
                if (courseVM.Course == null)
                {
                    return Forbid(); // Prevent users from editing others' courses
                }
                return View(courseVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(CourseVM courseVM)
        {
            var userId = _userManager.GetUserId(User);

            // Get the Term selection (ensure it belongs to the logged in user)
            Term selectedTerm = _uow.Term.Get(t => t.Id == courseVM.Course.TermId && t.UserId == userId);

            // Date validation
            if (courseVM.Course.StartDate > courseVM.Course.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be later than end date.");
            }


            if (ModelState.IsValid)
            {
                // Assign the selected Term to the Course object
                courseVM.Course.Term = selectedTerm;

                if (courseVM.Course.Id == 0)
                {
                    // Add a new course
                    _uow.Course.Add(courseVM.Course);
                }
                else
                {
                    // Ensure the user is not updating a course they don't own
                    var existingCourse = _uow.Course.Get(c => c.Id == courseVM.Course.Id);


                    if (existingCourse == null)
                    {
                        return Forbid();
                    }

                    // Update an existing course
                    existingCourse.Term = courseVM.Course.Term;
                    _uow.Course.Update(courseVM.Course);
                }

                _uow.Save();
                // Success notification 
                TempData["success"] = "Course added successfully";
                // Return to list page
                return RedirectToAction("Index", "DegreePlan");
            }
            else // Repopulate the drop-down if the model state returns invalid
            {
                courseVM.TermList = _uow.Term.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(courseVM);
            }
        }

        #endregion

        #region Course Detail

        public IActionResult Detail(int id)
        {
            var userId = _userManager.GetUserId(User);

            var course = _uow.Course.Get(c => c.Id == id && c.Term.UserId == userId, includeProperties: "Term");
            if (course == null)
            {
                return Forbid(); // Prevent users from viewing others' courses
            }

            var courseItems = _uow.CourseItem.GetAll(ci => ci.CourseId == id).OrderBy(ci => ci.DueDate ?? DateTime.MaxValue).ToList();

            var viewModel = new CourseDetailVM
            {
                Course = course,
                CourseItems = courseItems
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult MarkCourseItemAsComplete(int id, bool isCompleted)
        {
            var userId = _userManager.GetUserId(User);

            var courseItem = _uow.CourseItem.Get(ci => ci.Id == id && ci.Course.Term.UserId == userId);
            if (courseItem == null)
            {
                return NotFound();
            }

            courseItem.IsCompleted = isCompleted;
            _uow.Save();

            return Ok();
        }

        #endregion

        #region Delete/Remove Course
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
                //return Json(new { success = false, message = "Course not found." });
            }

            var userId = _userManager.GetUserId(User);

            Course? selectedCourse = _uow.Course.Get(c => c.Id == id && c.Term.UserId == userId); // Get by Id           
            if (selectedCourse == null)
            {
                return Forbid(); // Prevent users from deleting others' courses
            }

            return View(selectedCourse);
            //return Json(new { success = true, message = "Course deleted successfully." });
        }
        // Deleting a Course
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) 
        {
            Course? obj = _uow.Course.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }
            _uow.Course.Delete(obj);
            _uow.Save();
            // Success notification
            TempData["success"] = "Course deleted successfully";
            //Redirect to index
            return RedirectToAction("Index", "DegreePlan");
        }

        #endregion 
    }
}
