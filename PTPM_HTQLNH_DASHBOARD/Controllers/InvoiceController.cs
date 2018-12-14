using PTPM_HTQLNH_SHARE;
using PTPM_HTQLNH_SHARE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTPM_HTQLNH_DASHBOARD.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {

            var listInvoice = Helper.db.Invoices;
            return View(listInvoice);
        }
        public ActionResult Details(int id)
        {
            InvoiceViewModel invoice = new InvoiceViewModel();
            invoice.header = Helper.db.Invoices.Where(m => m.id == id).FirstOrDefault();
            invoice.details = Helper.db.InvoiceDetails.Where(m => m.invoice_id == id);
            return View(invoice);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm hóa đơn";
            return View();
        }

        public ActionResult Delete(string id)
        {
            var invoiceId = Convert.ToInt32(id);
            var invoice = Helper.db.Invoices.Where(m => m.id == invoiceId).FirstOrDefault();
            if (invoice == null) return RedirectToAction("Index");
            if (invoice.status == 1)
            {
                Helper.db.Invoices.Remove(invoice);
            }
            else
            {
                invoice.status = 3;
            }
            Helper.db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            var userId = Convert.ToInt32(id);

            var user = Helper.db.Users.Where(m => m.id == userId).FirstOrDefault();
            if (user == null) return RedirectToAction("Index");
            ViewBag.Title = "Sửa hóa đơn số " + user.Invoices;
            ViewBag.Action = "Edit";
            return View("Create", user);

        }
        [HttpPost]
        public ActionResult Edit(User user)
        {

            var oldUser = Helper.db.Users.Where(m => m.id == user.id).FirstOrDefault();
            ViewBag.Title = "Sửa thông tin của " + oldUser.login_name;
            if (user == null || user.password == null)
            {
                ViewBag.error = "Chưa nhập đầy đủ thông tin.";
                ViewBag.Action = "Edit";
                return View("Create", user);
            }
            oldUser.password = user.password;
            oldUser.status = user.status;

            Helper.db.Entry(oldUser).State = System.Data.Entity.EntityState.Modified;
            Helper.db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}