using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
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



        #region Upsert (Update/Insert)

        public IActionResult Upsert(int? id)
        {
            TermVM termVM = new()
            {
                Term = new Term()
            };
            if (id == null || id == 0) // if ID doesn't exist
            {
                //Create
                return View(termVM);
            }
            else
            {
                //Update
                termVM.Term = _uow.Term.Get(u => u.Id == id);
                return View(termVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(TermVM termVM)
        {
            // Date validation
            if (termVM.Term.StartDate > termVM.Term.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be later than end date.");
            }

            if (ModelState.IsValid)
            {
                if (termVM.Term.Id == 0)
                {
                    // Add a new term
                    _uow.Term.Add(termVM.Term);
                    TempData["success"] = "Term added successfully";
                }
                else
                {
                    // Update an existing term
                    _uow.Term.Update(termVM.Term);
                    _uow.Save();
                    // Success notification 
                    TempData["success"] = "Term updated successfully";
                }

                _uow.Save();
                // Return to list page
                return RedirectToAction("Index", "DegreePlan");
            }
            //If model state is not valid
            return View(termVM);
        }

        #endregion


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
