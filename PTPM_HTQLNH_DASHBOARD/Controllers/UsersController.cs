using PTPM_HTQLNH_SHARE;
using PTPM_HTQLNH_SHARE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTPM_HTQLNH_DASHBOARD.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
          /* User u1 =  Helper.db.Users.Where(m => m.id == 1).FirstOrDefault();
            User u2 = (from u in Helper.db.Users where u.id == 1 select u).FirstOrDefault();
            var join = (from u_r in Helper.db.User_Restaurant
                        join r in Helper.db.Restaurants on u_r.res_id equals r.id
                        where u_r.user_id == 1
                        select r).ToList(); ; */
            var listUser = Helper.db.Users;
            return View(listUser);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm người dùng";
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            ViewBag.Title = "Thêm người dùng";
            if (user== null || user.login_name == null || user.password == null)
            {
                ViewBag.error = "Chưa nhập đầy đủ thông tin.";
                return View(user);
            }
            if(Helper.db.Users.Where(m => m.login_name == user.login_name).FirstOrDefault() != null)
            {
                ViewBag.error = "Tài khoản đã có người sử dụng.";
                return View(user);
            }
            user.date_create = DateTime.Now;
            Helper.db.Users.Add(user);
            Helper.db.SaveChanges();


            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            var userId = Convert.ToInt32(id);
            var user = Helper.db.Users.Where(m => m.id == userId).FirstOrDefault();
            if(user== null) return RedirectToAction("Index");

            Helper.db.Users.Remove(user);
            Helper.db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            var userId = Convert.ToInt32(id);

            var user = Helper.db.Users.Where(m => m.id == userId).FirstOrDefault();
            if (user == null) return RedirectToAction("Index");
            ViewBag.Title = "Sửa thông tin của "+ user.login_name;
            ViewBag.Action = "Edit";
            return View("Create",user);

        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
           
            var oldUser = Helper.db.Users.Where(m => m.id == user.id).FirstOrDefault();
            ViewBag.Title = "Sửa thông tin của " + oldUser.login_name;
            if (user == null ||  user.password == null)
            {
                ViewBag.error = "Chưa nhập đầy đủ thông tin.";
                ViewBag.Action = "Edit";
                return View("Create",user);
            }
            oldUser.password = user.password;
            oldUser.status = user.status;

            Helper.db.Entry(oldUser).State = System.Data.Entity.EntityState.Modified;
            Helper.db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}