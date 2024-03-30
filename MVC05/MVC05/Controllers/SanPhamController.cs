using Microsoft.AspNetCore.Mvc;
using MVC05.Models;

namespace MVC05.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly DbShop4TrainingContext _db;
        public SanPhamController(DbShop4TrainingContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public JsonResult dssp()
        {
            try
            {
                var dsSP = _db.TblHanghoas.ToList();
                return Json(new { code = 200, dsSP = dsSP, msg = "Lấy danh sách sản phẩm thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách sản phẩm thất bại" + ex.Message });
            }
        }
    }
}
