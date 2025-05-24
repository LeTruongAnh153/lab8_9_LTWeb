using System.Linq;
using System.Web.Mvc;
using Lab04.Models; // Đảm bảo đúng namespace chứa DataContext và model

namespace Lab04.Controllers
{
    public class AdminController : Controller
    {
        // Kết nối CSDL (đổi chuỗi nếu cần)
        DataClasses2DataContext db = new DataClasses2DataContext(
            "Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;"
        );

        // GET: Admin/Login (hiển thị form đăng nhập)
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login (xử lý đăng nhập)
        [HttpPost]
        public ActionResult Login(string TaiKhoan, string MatKhau)
        {
            var admin = db.Admins.SingleOrDefault(a => a.TaiKhoan == TaiKhoan && a.MatKhau == MatKhau);
            if (admin != null)
            {
                Session["Admin"] = admin;
                return RedirectToAction("Index");
            }

            ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        // Trang chính sau khi đăng nhập
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            return View();
        }

        // Trang Dashboard hiển thị danh sách sách
        public ActionResult Dashboard()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            var books = db.Saches.ToList();
            return View(books);
        }

        // Trang User hiển thị danh sách Admins
        public ActionResult Users()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            var users = db.Admins.ToList();
            return View(users);
        }
        public ActionResult Product()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            var products = db.Saches.ToList(); // Lấy sách từ DB
            return View(products);             // Truyền vào View
        }
        public ActionResult Order()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            var orders = db.DonDatHangs.ToList(); // Đổi `DonHangs` nếu bảng của bạn có tên khác
            return View(orders);
        }

    }
}
