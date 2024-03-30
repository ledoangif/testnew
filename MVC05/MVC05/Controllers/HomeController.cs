using Microsoft.AspNetCore.Mvc;
using MVC05.Models;
using System.Diagnostics;

namespace MVC05.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbShop4TrainingContext _context;
        public HomeController(ILogger<HomeController> logger, DbShop4TrainingContext context)
        {
            _logger = logger;
            _context=context;
        }
  
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UploadImage()
        {
            List<TblHanghoa> hanghoas =_context.TblHanghoas.ToList();
            return View(hanghoas);
        }
        [HttpPost]
        public IActionResult UploadImage (IFormFile file,TblHanghoa hanghoa) 
        {
            if(file != null && file.Length>0) 
            {
                var finame = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "img",finame);
                using(var stream = new FileStream(filePath,FileMode.Create))
                {
                    file.CopyTo(stream);
                    hanghoa.SAnhMinhHoa = finame;
                }
                var tblHang = new TblHanghoa
                {
                    STenhang = hanghoa.STenhang,
                    FGianiemyet = hanghoa.FGianiemyet,
                    SDacdiem = hanghoa.SDacdiem,
                    SXuatxu = hanghoa.SXuatxu,
                    SAnhMinhHoa = hanghoa.SAnhMinhHoa,
                };
                _context.TblHanghoas.Add(tblHang);
                _context.SaveChanges();
                return RedirectToAction("UploadImage");
            }
            return View("Index");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            List<TblChitiethoadon> tblChitiethoadon = _context.TblChitiethoadons.Where(x=>x.FkIHanghoaId==id).ToList();
            _context.TblChitiethoadons.RemoveRange(tblChitiethoadon);
            _context.SaveChanges();

            var tblHanghoas = _context.TblHanghoas.Where(row =>row.PkIHanghoaId==id).FirstOrDefault();
            _context.TblHanghoas.Remove(tblHanghoas);
            _context.SaveChanges();
            return Json(new { success = id });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}