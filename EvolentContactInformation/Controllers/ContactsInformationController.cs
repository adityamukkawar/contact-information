using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.Repository;

namespace EvolentContactInformation.Controllers
{
    public class ContactsInformationController : Controller
    {
        private readonly UnitOfWork _UOW;


        public ContactsInformationController(UnitOfWork unitOfWork)
        {
            _UOW = unitOfWork;
        }

        // GET: ContactsInformation
        public async Task<IActionResult> Index()
        {
            return View(await _UOW.ContactRepository.Get().ToListAsync());
        }

        // GET: ContactsInformation/Details/5
        public IActionResult Details(int? id)
        {
            var contact = _UOW.ContactRepository.GetByID(id);
            return View(contact);
        }

        // GET: ContactsInformation/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,FirstName,LastName,EmailId,PhoneNumber,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Status = true;
                _UOW.ContactRepository.Insert(contact);
                _UOW.ContactRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        public ActionResult Edit(int? id)
        {
            var contact = _UOW.ContactRepository.Get().Where(a => a.Id == id).Single();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,FirstName,LastName,EmailId,PhoneNumber,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _UOW.ContactRepository.Update(contact);
                _UOW.ContactRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }


        public ActionResult Delete(int? id)
        {
            var contact = _UOW.ContactRepository.GetByID(id);
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contact = _UOW.ContactRepository.GetByID(id);
            _UOW.ContactRepository.Delete(contact);
            _UOW.ContactRepository.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
