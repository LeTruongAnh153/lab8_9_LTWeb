// Controller: DonDatHangController.cs
using Lab04.Models;
using System.Web.Mvc;
using System.Linq;
using System;

public class DonDatHangController : Controller
{
    DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

    public ActionResult Index() => View(db.DonDatHangs.ToList());
    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(DonDatHang ddh)
    {
        db.DonDatHangs.InsertOnSubmit(ddh);
        db.SubmitChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
        var dh = db.DonDatHangs.SingleOrDefault(m => m.SoDH == id);
        if (dh == null)
            return HttpNotFound();
        return View(dh);
    }

    public ActionResult Edit(int id) => View(db.DonDatHangs.SingleOrDefault(m => m.SoDH == id));

    [HttpPost]
    public ActionResult Edit(DonDatHang ddh)
    {
        var d = db.DonDatHangs.SingleOrDefault(m => m.SoDH == ddh.SoDH);
        if (d != null)
        {
            d.NgayDH = ddh.NgayDH;
            d.TinhTrang = ddh.TinhTrang;
            d.MaKH = ddh.MaKH;
            db.SubmitChanges();
        }
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var dh = db.DonDatHangs.SingleOrDefault(m => m.SoDH == id);
        if (dh == null)
            return HttpNotFound();

        // Xóa các chi tiết đơn hàng trước
        var chiTiets = db.ChiTietDonDatHangs.Where(c => c.SoDH == id);
        db.ChiTietDonDatHangs.DeleteAllOnSubmit(chiTiets);

        try
        {
            db.DonDatHangs.DeleteOnSubmit(dh);
            db.SubmitChanges();
        }
        catch (Exception)
        {
            ViewBag.Error = "Không thể xóa đơn hàng vì xảy ra lỗi liên kết dữ liệu.";
            return View("Error");
        }

        return RedirectToAction("Index");
    }

}
