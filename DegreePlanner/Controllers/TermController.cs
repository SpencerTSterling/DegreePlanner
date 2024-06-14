using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreePlanner.Controllers
{
    public class TermController : Controller
    {
        private readonly IUnitOfWork _uow; //unit of work
        public TermController(IUnitOfWork unitOfWork)
        { 
            _uow = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Term> objTermList = _uow.Term.GetAll().ToList();
            return View(objTermList);
        }

        #region Create/Add Term
        public IActionResult Create()
        {
            return View();
        }
        // Creating a Term
        [HttpPost]
        public IActionResult Create(Term obj)
        {
            // Date validation
            if (obj.StartDate > obj.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be later than end date.");
            }

            if (ModelState.IsValid)
            {
                _uow.Term.Add(obj);
                _uow.Save();
                // Success notification 
                TempData["success"] = "Term added successfully";
                // Return to list page
                return RedirectToAction("Index", "Term");
            }
            return View();
        }
        #endregion

        #region Edit/Update Term
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Term? selectedTerm = _uow.Term.Get(u => u.Id == id); // get by Id
            if (selectedTerm == null)
            {
                return NotFound();
            }

            return View(selectedTerm);
        }
        //Editing a Term
        [HttpPost]
        public IActionResult Edit(Term obj) 
        {
            // Date validation
            if (obj.StartDate > obj.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be later than end date.");
            }

            if (ModelState.IsValid)
            {
                _uow.Term.Update(obj);
                _uow.Save();
                // Success notification
                TempData["success"] = "Term edited successfully";
                // Return to list page
                return RedirectToAction("Index", "Term");
            }
            return View();
        }

        #endregion


        #region Delete/Remove Term
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Term? selectedTerm = _uow.Term.Get(u => u.Id == id); // Get by Id         
            if (selectedTerm == null)
            {
                return NotFound();
            }

            return View(selectedTerm);
        }
        // Deleting a Term
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) 
        {
            Term? obj = _uow.Term.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }
            _uow.Term.Delete(obj);
            _uow.Save();
            // Success notification
            TempData["success"] = "Term deleted successfully";
            //Redirect to index
            return RedirectToAction("Index", "Term");
        }

        #endregion 
    }
}
