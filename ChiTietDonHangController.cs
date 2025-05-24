using Lab04.Models;
using System.Linq;
using System.Web.Mvc;

public class ChiTietDonDatHangController : Controller
{
    DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

    // GET: ChiTietDonDatHang
    public ActionResult Index()
    {
        var ds = db.ChiTietDonDatHangs.ToList();
        return View(ds);
    }

    // GET: Create
    public ActionResult Create()
    {
        ViewBag.SoDH = new SelectList(db.DonDatHangs, "SoDH", "SoDH");
        ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");
        return View();
    }

    // POST: Create
    [HttpPost]
    public ActionResult Create(ChiTietDonDatHang ct)
    {
        if (ModelState.IsValid)
        {
            db.ChiTietDonDatHangs.InsertOnSubmit(ct);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        // Nếu có lỗi, nạp lại dropdown
        ViewBag.SoDH = new SelectList(db.DonDatHangs, "SoDH", "SoDH", ct.SoDH);
        ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", ct.MaSach);
        return View(ct);
    }

    // GET: Details
    public ActionResult Details(int soDH, int maSach)
    {
        var ct = db.ChiTietDonDatHangs.SingleOrDefault(c => c.SoDH == soDH && c.MaSach == maSach);
        if (ct == null) return HttpNotFound();
        return View(ct);
    }

    // GET: Edit
    public ActionResult Edit(int soDH, int maSach)
    {
        var ct = db.ChiTietDonDatHangs.SingleOrDefault(c => c.SoDH == soDH && c.MaSach == maSach);
        if (ct == null) return HttpNotFound();

        ViewBag.SoDH = new SelectList(db.DonDatHangs, "SoDH", "SoDH", ct.SoDH);
        ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", ct.MaSach);
        return View(ct);
    }

    // POST: Edit
    [HttpPost]
    public ActionResult Edit(ChiTietDonDatHang ct)
    {
        if (ModelState.IsValid)
        {
            var old = db.ChiTietDonDatHangs.SingleOrDefault(c => c.SoDH == ct.SoDH && c.MaSach == ct.MaSach);
            if (old != null)
            {
                old.SoLuong = ct.SoLuong;
                old.DonGia = ct.DonGia;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
        }

        ViewBag.SoDH = new SelectList(db.DonDatHangs, "SoDH", "SoDH", ct.SoDH);
        ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", ct.MaSach);
        return View(ct);
    }

    // GET: Delete
    public ActionResult Delete(int soDH, int maSach)
    {
        var ct = db.ChiTietDonDatHangs.SingleOrDefault(c => c.SoDH == soDH && c.MaSach == maSach);
        if (ct == null) return HttpNotFound();
        return View(ct);
    }

    // POST: Delete
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int soDH, int maSach)
    {
        var ct = db.ChiTietDonDatHangs.SingleOrDefault(c => c.SoDH == soDH && c.MaSach == maSach);
        if (ct != null)
        {
            db.ChiTietDonDatHangs.DeleteOnSubmit(ct);
            db.SubmitChanges();
        }
        return RedirectToAction("Index");
    }
}
