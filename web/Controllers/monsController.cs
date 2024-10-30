using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Views
{
    public class monsController : Controller
    {
         seoEntities db = new seoEntities();

        // GET: mons
        public ActionResult Index()
        {
            return View(db.mon.ToList());
        }

        // GET: mons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = db.mon.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // GET: mons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Tenmon,Gia,Ghichu,Hinh,Loai,Diachi,TP")] mon mon, HttpPostedFileBase imgFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string path = uploadimage(imgFile);
        //        db.mons.Add(mon);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(mon);
        //}
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(mon m, HttpPostedFileBase imgFile)
        {
            string path = uploadimage(imgFile);
            mon n = new mon();
            if (path.Equals("-1"))
            {

            }
            else
            {
                n.ID = m.ID;
                n.Tenmon = m.Tenmon;
                n.Gia = m.Gia;
                n.Ghichu=m.Ghichu;
                n.Hinh = path;
                n.Loai = m.Loai;
                n.Diachi= m.Diachi;
                n.TP = m.TP;
                db.mon.Add(n);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public string uploadimage(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Image"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Image/" + random + Path.GetFileName(file.FileName);

                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {

                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
        }
        // GET: mons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = db.mon.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // POST: mons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tenmon,Gia,Ghichu,Hinh,Loai")] mon mon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mon);
        }

        // GET: mons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = db.mon.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // POST: mons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            mon mon = db.mon.Find(id);
            db.mon.Remove(mon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
