// Controller: TacGiaController.cs
using Lab04.Models;
using System.Web.Mvc;
using System.Linq;
using System;
public class TacGiaController : Controller
{
    DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

    public ActionResult Index() => View(db.TacGias.ToList());
    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(TacGia tg)
    {
        db.TacGias.InsertOnSubmit(tg);
        db.SubmitChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id) => View(db.TacGias.SingleOrDefault(m => m.MaTG == id));

    [HttpPost]
    public ActionResult Edit(TacGia tg)
    {
        var t = db.TacGias.SingleOrDefault(m => m.MaTG == tg.MaTG);
        if (t != null)
        {
            t.TenTG = tg.TenTG;
            t.DiaChi = tg.DiaChi;
            t.DienThoai = tg.DienThoai;
            db.SubmitChanges();
        }
        return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
        TacGia tg = db.TacGias.SingleOrDefault(m => m.MaTG == id);
        if (tg == null)
            return HttpNotFound();

        return View(tg);
    }
    public ActionResult Delete(int id)
    {
        var t = db.TacGias.SingleOrDefault(m => m.MaTG == id);
        if (t == null)
            return HttpNotFound();

        var related = db.TG_SACHes.Where(r => r.MaTG == id);
        db.TG_SACHes.DeleteAllOnSubmit(related);

        try
        {
            db.TacGias.DeleteOnSubmit(t);
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Không thể xóa tác giả vì có liên kết đến sách.";
            return View("Error");
        }

        return RedirectToAction("Index");
    }

}