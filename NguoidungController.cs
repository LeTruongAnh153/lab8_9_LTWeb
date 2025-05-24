using System;
using System.Linq;
using System.Web.Mvc;
using Lab04.Models;


namespace Lab04.Controllers
{
    public class NguoidungController : Controller
    {
        DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

        public ActionResult Index()
        {
            return View();
        }

        // GET: Dangky
        public ActionResult Dangky()
        {
            return View();
        }

        // POST: Dangky
        [HttpPost]
        public ActionResult Dangky(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return View(kh);
        }

        // GET: Dangnhap
        public ActionResult Dangnhap()
        {
            return View();
        }

        // POST: Dangnhap
        [HttpPost]
        public ActionResult Dangnhap(KhachHang kh)
        {
            var user = db.KhachHangs.SingleOrDefault(m => m.TaiKhoan == kh.TaiKhoan && m.MatKhau == kh.MatKhau);
            if (user != null)
            {
                Session["Taikhoan"] = user;
                return RedirectToAction("Index", "BookStore"); // Chuyển hướng về trang chủ
            }

            ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }

        // Đăng xuất
        public ActionResult Dangxuat()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
