using Lab04.Models;
using System.Web.Mvc;
using System.Linq;
using System;

public class SachController : Controller
{
    DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

    public ActionResult Index()
    {
        return View(db.Saches.ToList());
    }
    public ActionResult Details(int id)
    {
        var sach = db.Saches.SingleOrDefault(s => s.MaSach == id);
        if (sach == null) return HttpNotFound();
        return View(sach); // View này là ChiTiet.cshtml
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Sach s)
    {
        db.Saches.InsertOnSubmit(s);
        db.SubmitChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
        var sach = db.Saches.SingleOrDefault(m => m.MaSach == id);
        return View(sach);
    }

    [HttpPost]
    public ActionResult Edit(Sach s)
    {
        var sach = db.Saches.SingleOrDefault(m => m.MaSach == s.MaSach);
        if (sach != null)
        {
            sach.TenSach = s.TenSach;
            sach.DonGia = s.DonGia;
            // Cập nhật các trường khác nếu cần
            db.SubmitChanges();
        }
        return RedirectToAction("Index");
    }

    // GET: Sach/Delete/5
    public ActionResult Delete(int id)
    {
        var sach = db.Saches.SingleOrDefault(s => s.MaSach == id);
        if (sach == null)
            return HttpNotFound();
        return View(sach);
    }

    // POST: Sach/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var sach = db.Saches.SingleOrDefault(s => s.MaSach == id);
        if (sach == null)
            return HttpNotFound();

        // Xóa các liên kết trước
        var ctddh = db.ChiTietDonDatHangs.Where(c => c.MaSach == id);
        db.ChiTietDonDatHangs.DeleteAllOnSubmit(ctddh);

        var tgSach = db.TG_SACHes.Where(t => t.MaSach == id);
        db.TG_SACHes.DeleteAllOnSubmit(tgSach);

        try
        {
            db.Saches.DeleteOnSubmit(sach);
            db.SubmitChanges();
        }
        catch (Exception)
        {
            ViewBag.Error = "Không thể xóa sách vì có liên kết dữ liệu.";
            return View("Error");
        }

        return RedirectToAction("Index");
    }
}

