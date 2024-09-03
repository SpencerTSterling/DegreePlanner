using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using DegreePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DegreePlanner.Controllers
{
    [Authorize] // Ensure only logged-in users can access these actions
    public class TermController : Controller
    {
        private readonly IUnitOfWork _uow; //unit of work
        private readonly UserManager<IdentityUser> _userManager; // inject UserManager
        public TermController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        { 
            _uow = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Get logged in user's ID
            var userId = _userManager.GetUserId(User);

            // Fetch terms only assocaited with the logged in user
            List<Term> objTermList = _uow.Term.GetAll(u => u.UserId == userId).ToList();
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

            // If all feilds are valid: 
            if (ModelState.IsValid)
            {
                // Get the logged-in user's ID
                var userId = _userManager.GetUserId(User);

                if (termVM.Term.Id == 0)
                {
                    // Set the UserId for the new Term
                    termVM.Term.UserId = userId;

                    // Add a new term
                    _uow.Term.Add(termVM.Term);
                    TempData["success"] = "Term added successfully";
                }
                else
                {
                    // Ensure the UserId is not modified during updates
                    var existingTerm = _uow.Term.Get(u => u.Id == termVM.Term.Id);

                    if (existingTerm != null && existingTerm.UserId == userId)// if the existing term exists & matches User ID
                    {
                        // Update only if the term belongs to the logged-in user
                        _uow.Term.Update(termVM.Term);
                        TempData["success"] = "Term updated successfully";
                    }
                    else
                    {
                        return Forbid();
                    }

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
