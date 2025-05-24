using System.Linq;
using System.Web.Mvc;
using Lab04.Models;

namespace Lab04.Controllers
{
    public class BookStoreController : Controller
    {
        // Khởi tạo data context dùng lại cho cả controller
        private DataClasses2DataContext db = new DataClasses2DataContext("Data Source=.\\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True;");

        public ActionResult Index()
        {
            var listSachMoi = db.Saches
                                .Where(s => s.Hinh != null)
                                .OrderByDescending(s => s.NgayCapNhat)
                                .Take(5)
                                .ToList();

            return View(listSachMoi);
        }

        public ActionResult Details(int id)
        {
            var sach = db.Saches.SingleOrDefault(s => s.MaSach == id);
            if (sach == null) return HttpNotFound();
            return View(sach); // View này tên là Details.cshtml
        }
    }
}
