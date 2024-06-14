using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegreePlannerWeb.Controllers
{
    public class CourseItemController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CourseItemController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public IActionResult Index()
        {
            List<CourseItem> objCourseItemList = _uow.CourseItem.GetAll(includeProperties: "Course").ToList();
            return View(objCourseItemList);
        }

        #region Upsert (Update/Insert)
        public IActionResult Upsert(int? id)
        {
            CourseItemVM courseitemVM = new()
            {
                CourseList = _uow.Course.GetAll().Select(u => new SelectListItem
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
                courseitemVM.CourseItem = _uow.CourseItem.Get(u => u.Id == id);
                return View(courseitemVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(CourseItemVM courseitemVM)
        {
            // Get the course selection
            Course selectedCourse = _uow.Course.Get(t => t.Id == courseitemVM.CourseItem.CourseId);

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
