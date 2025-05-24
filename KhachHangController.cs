// Controller: KhachHangController.cs
using Lab04.Models;
using System.Web.Mvc;
using System.Linq;
using System;
public class KhachHangController : Controller
{
    DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

    public ActionResult Index() => View(db.KhachHangs.ToList());
    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(KhachHang kh)
    {
        db.KhachHangs.InsertOnSubmit(kh);
        db.SubmitChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
        var kh = db.KhachHangs.SingleOrDefault(k => k.MaKH == id);
        if (kh == null)
            return HttpNotFound();
        return View(kh); // Trả về view Details.cshtml với model là 1 khách hàng
    }
    public ActionResult Edit(int id) => View(db.KhachHangs.SingleOrDefault(m => m.MaKH == id));

    [HttpPost]
    public ActionResult Edit(KhachHang kh)
    {
        var k = db.KhachHangs.SingleOrDefault(m => m.MaKH == kh.MaKH);
        if (k != null)
        {
            k.HoTen = kh.HoTen;
            k.DiaChi = kh.DiaChi;
            k.DienThoai = kh.DienThoai;
            k.Email = kh.Email;
            k.MatKhau = kh.MatKhau;
            db.SubmitChanges();
        }
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var kh = db.KhachHangs.SingleOrDefault(m => m.MaKH == id);
        if (kh == null)
            return HttpNotFound();

        // Tìm các đơn đặt hàng của khách
        var donHangs = db.DonDatHangs.Where(d => d.MaKH == id);

        foreach (var dh in donHangs.ToList())
        {
            // Xóa chi tiết đơn hàng trước
            var chiTiet = db.ChiTietDonDatHangs.Where(c => c.SoDH == dh.SoDH);
            db.ChiTietDonDatHangs.DeleteAllOnSubmit(chiTiet);

            db.DonDatHangs.DeleteOnSubmit(dh);
        }

        try
        {
            db.KhachHangs.DeleteOnSubmit(kh);
            db.SubmitChanges();
        }
        catch (Exception)
        {
            ViewBag.Error = "Không thể xóa khách hàng vì có đơn hàng liên kết.";
            return View("Error");
        }

        return RedirectToAction("Index");
    }
}