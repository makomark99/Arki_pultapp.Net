using Microsoft.AspNetCore.Mvc;
using PultApp.Data;
using PultApp.Models;
using static PultApp.Controllers.AdminController;

namespace PultApp.Controllers
{

    [NotLoggedInFilter]
    public class FormController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FormController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public IActionResult Form()
        {
            return View();
        }

        //POST 1
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Form(Review obj)
        {
            _db.Reviews.Add(obj);
            _db.SaveChanges();
            System.Threading.Thread.Sleep(3000);
            return Redirect("~/Home/Index");
        }
    }
}
