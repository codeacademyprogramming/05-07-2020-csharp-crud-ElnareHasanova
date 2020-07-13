using aspnetmvcmethods.Contexts;
using aspnetmvcmethods.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace aspnetmvcmethods.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        UserContext _db = new UserContext();
        public ActionResult Index()
        {
            var userModel = new UserModel()
            {
                Users = _db.Users.ToList()
            };
            return View(userModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
            _db.Users.Add(user);
            _db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(400);
            }


            return RedirectToAction("Index");
        }


        public ActionResult GetUser (int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            return View(user);
        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //public ActionResult Edit()
        //{
        //    return View(user);
        //}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                _db.SaveChanges();
            }

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {

            if (ModelState.IsValid)
            {
            var user_ = _db.Users.FirstOrDefault(u => u.Id == user.Id);

            if (user_ == null)
            {
                return new HttpStatusCodeResult(404);

            }
                user_ = user;

                _db.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();

                    return RedirectToAction("Index");

            }
            return new HttpStatusCodeResult(400);
        }
    }
}