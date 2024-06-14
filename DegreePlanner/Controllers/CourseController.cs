using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegreePlanner.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _uow; //unit of work
        public CourseController(IUnitOfWork unitOfWork)
        { 
            _uow = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Course> objCourseList = _uow.Course.GetAll(includeProperties: "Term").ToList();
            return View(objCourseList);
        }


        #region Upsert (Update/Insert)
        public IActionResult Upsert(int? id)
        {

            CourseVM courseVM = new()
            {
                TermList = _uow.Term.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Course = new Course()
            };
            if (id == null || id == 0) // if ID doesn't exist
            {
                // Create
                return View(courseVM);
            }
            else
            {
                // Update
                courseVM.Course = _uow.Course.Get(u => u.Id == id);
                return View(courseVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(CourseVM courseVM)
        {
            // Get the Term selection
            Term selectedTerm = _uow.Term.Get(t => t.Id == courseVM.Course.TermId);

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
                    // Update an existing course
                    _uow.Course.Update(courseVM.Course);
                }



                //_uow.Course.Add(courseVM.Course);
                _uow.Save();
                // Success notification 
                TempData["success"] = "Course added successfully";
                // Return to list page
                return RedirectToAction("Index", "Course");
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
            var course = _uow.Course.Get(c => c.Id == id, includeProperties: "Term");
            if (course == null)
            {
                return NotFound();
            }

            var courseItems = _uow.CourseItem.GetAll(ci => ci.CourseId == id).OrderBy(ci => ci.DueDate ?? DateTime.MaxValue).ToList();

            var viewModel = new CourseDetailVM
            {
                Course = course,
                CourseItems = courseItems
            };

            return View(viewModel);
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
            Course? selectedCourse = _uow.Course.Get(u => u.Id == id); // Get by Id         
            if (selectedCourse == null)
            {
                return NotFound();
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
            return RedirectToAction("Index", "Course");
        }

        #endregion 
    }
}
