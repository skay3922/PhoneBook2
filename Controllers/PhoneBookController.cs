using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services;
using PhoneBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace PhoneBook.Controllers
{
    [Authorize]
    public class PhoneBookController : Controller
    {
        private readonly IPhoneBookService phoneBookService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhoneBookController(IPhoneBookService phoneBookService, UserManager<ApplicationUser> userManager)
        {
            this.phoneBookService = phoneBookService;
            this._userManager = userManager;
        }

        public IActionResult Index(string searchString)
        {
            List<PhoneBookModel> model = new List<PhoneBookModel>();
            phoneBookService.GetPhoneBooks().Where(x => x.UserId == _userManager.GetUserId(User)).ToList().ForEach(pb =>
               {
                   PhoneBookModel phoneBook = new PhoneBookModel
                   {
                       Id = pb.Id,
                       FirstName = pb.FirstName,
                       LastName = pb.LastName,
                       Email = pb.Email,
                       Phone = pb.Phone,
                       Organization = pb.Organization,
                       MobilePhone = pb.MobilePhone,
                       HomePhone = pb.HomePhone,
                       WorkPhone = pb.WorkPhone

                   };
                   model.Add(phoneBook);
               });

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Phone.Contains(searchString)).ToList();
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PhoneBookModel model)
        {
            if (ModelState.IsValid)
            {
                PhoneBook.Data.PhoneBook phoneBook = new PhoneBook.Data.PhoneBook
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Organization = model.Organization,
                    MobilePhone = model.MobilePhone,
                    HomePhone = model.HomePhone,
                    WorkPhone = model.WorkPhone,
                    UserId = _userManager.GetUserId(User)
                };
                phoneBookService.InsertPhoneBook(phoneBook);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            PhoneBookModel model = new PhoneBookModel();
            if (id.HasValue && id != 0)
            {
                PhoneBook.Data.PhoneBook pbEntity = phoneBookService.GetPhoneBook(id.Value);
                if (pbEntity != null)
                {
                    model.FirstName = pbEntity.FirstName;
                    model.LastName = pbEntity.LastName;
                    model.Email = pbEntity.Email;
                    model.MobilePhone = pbEntity.MobilePhone;
                    model.Organization = pbEntity.Organization;
                    model.Phone = pbEntity.Phone;
                    model.WorkPhone = pbEntity.WorkPhone;
                    model.HomePhone = pbEntity.HomePhone;
                }
                else
                    return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, PhoneBookModel model)
        {
            if (id != model.Id)
                return NotFound();

            PhoneBook.Data.PhoneBook pbEntity = phoneBookService.GetPhoneBook(model.Id);
            if (ModelState.IsValid)
            {
                pbEntity.FirstName = model.FirstName;
                pbEntity.LastName = model.LastName;
                pbEntity.Email = model.Email;
                pbEntity.MobilePhone = model.MobilePhone;
                pbEntity.Organization = model.Organization;
                pbEntity.Phone = model.Phone;
                pbEntity.WorkPhone = model.WorkPhone;
                pbEntity.HomePhone = model.HomePhone;

                phoneBookService.UpdatePhoneBook(pbEntity);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            PhoneBookModel model = new PhoneBookModel();

            PhoneBook.Data.PhoneBook pbEntity = phoneBookService.GetPhoneBook(id);
            model.FirstName = pbEntity.FirstName;
            model.LastName = pbEntity.LastName;
            model.Email = pbEntity.Email;
            model.MobilePhone = pbEntity.MobilePhone;
            model.Organization = pbEntity.Organization;
            model.Phone = pbEntity.Phone;
            model.WorkPhone = pbEntity.WorkPhone;
            model.HomePhone = pbEntity.HomePhone;

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PhoneBook.Data.PhoneBook pbEntity = phoneBookService.GetPhoneBook(id);
            PhoneBookModel model = new PhoneBookModel
            {
                FirstName = pbEntity.FirstName,
                LastName = pbEntity.LastName,
                Email = pbEntity.Email,
                MobilePhone = pbEntity.MobilePhone,
                Organization = pbEntity.Organization,
                Phone = pbEntity.Phone,
                WorkPhone = pbEntity.WorkPhone,
                HomePhone = pbEntity.HomePhone
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id, string m)
        {
            phoneBookService.DeletePhoneBook(id);
            return RedirectToAction("Index");

        }
    }
}