// Controller: ChuDeController.cs
using Lab04.Models;
using System.Web.Mvc;
using System.Linq;

public class ChuDeController : Controller
{
    DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");


    public ActionResult Index()
    {
        return View(db.ChuDes.ToList());
    }

    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(ChuDe cd)
    {
        db.ChuDes.InsertOnSubmit(cd);
        db.SubmitChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id) => View(db.ChuDes.SingleOrDefault(c => c.MaCD == id));

    [HttpPost]
    public ActionResult Edit(ChuDe cd)
    {
        var c = db.ChuDes.SingleOrDefault(m => m.MaCD == cd.MaCD);
        if (c != null)
        {
            c.TenChuDe = cd.TenChuDe;
            c.MoTa = cd.MoTa;
            db.SubmitChanges();
        }
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var cd = db.ChuDes.SingleOrDefault(m => m.MaCD == id);
        db.ChuDes.DeleteOnSubmit(cd);
        db.SubmitChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Details(int id) => View(db.ChuDes.SingleOrDefault(m => m.MaCD == id));


    public ActionResult SelectChuDe()
    {
        var listChuDe = db.ChuDes.ToList();
        ViewBag.ChuDeList = new SelectList(listChuDe, "MaCD", "TenChuDe");
        return View();
    }

    [HttpPost]
    public ActionResult SelectChuDe(int selectedChuDeId)
    {
        var listChuDe = db.ChuDes.ToList();
        ViewBag.ChuDeList = new SelectList(listChuDe, "MaCD", "TenChuDe");

        var selectedChuDe = db.ChuDes.SingleOrDefault(c => c.MaCD == selectedChuDeId);
        if (selectedChuDe != null)
        {
            return View(selectedChuDe);
        }

        return View(); // Nếu không có chủ đề, vẫn cần ViewBag để dropdown không lỗi
    }


}



