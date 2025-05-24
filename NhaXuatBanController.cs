// Controller: NhaXuatBanController.cs
using Lab04.Models;
using System.Web.Mvc;
using System.Linq;
using System;

public class NhaXuatBanController : Controller
{
    DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

    public ActionResult Index() => View(db.NhaXuatBans.ToList());
    public ActionResult Create() => View();

    
    [HttpPost]
    public ActionResult Create([Bind(Exclude = "MaNXB")] NhaXuatBan nxb)
    {
        db.NhaXuatBans.InsertOnSubmit(nxb);
        db.SubmitChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id) => View(db.NhaXuatBans.SingleOrDefault(m => m.MaNXB == id));

    [HttpPost]
    public ActionResult Edit(NhaXuatBan nxb)
    {
        var x = db.NhaXuatBans.SingleOrDefault(m => m.MaNXB == nxb.MaNXB) ;
        if (x != null)
        {
            x.TenNXB = nxb.TenNXB;
            x.DiaChi = nxb.DiaChi;
            x.DienThoai = nxb.DienThoai;
            db.SubmitChanges();
        }
        return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
        var nxb = db.NhaXuatBans.SingleOrDefault(m => m.MaNXB == id);
        if (nxb == null)
            return HttpNotFound();
        return View(nxb);
    }

    public ActionResult Delete(int id)
    {
        var nxb = db.NhaXuatBans.SingleOrDefault(m => m.MaNXB == id);
        if (nxb == null)
            return HttpNotFound();

        // Xóa các sách thuộc nhà xuất bản này (nếu muốn)
        var saches = db.Saches.Where(s => s.MaNXB == id);

        // Nếu cần, cũng nên xóa các bảng liên quan đến sách trước (VD: TG_SACH, ChiTietDonDatHang)
        foreach (var sach in saches.ToList())
        {
            var ctddh = db.ChiTietDonDatHangs.Where(c => c.MaSach == sach.MaSach);
            db.ChiTietDonDatHangs.DeleteAllOnSubmit(ctddh);

            var tgSach = db.TG_SACHes.Where(t => t.MaSach == sach.MaSach);
            db.TG_SACHes.DeleteAllOnSubmit(tgSach);

            db.Saches.DeleteOnSubmit(sach);
        }

        try
        {
            db.NhaXuatBans.DeleteOnSubmit(nxb);
            db.SubmitChanges();
        }
        catch (Exception)
        {
            ViewBag.Error = "Không thể xóa nhà xuất bản vì có sách đang sử dụng.";
            return View("Error");
        }

        return RedirectToAction("Index");
    }
}