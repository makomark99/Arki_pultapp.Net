using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PultApp.Data;
using System.Security.Cryptography;
using System.Text;

namespace PultApp.Controllers
{
    public class AdminController : Controller
    {
       
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [LoginFilter]
        public class ReviewController: Controller
        {
            public IActionResult Charts()
            {
                return View();
            }
            public IActionResult Index()
            {
                return View();
            }
        }

        //Password hash
        public static string HashString(string inputString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {

            var userData = _db.Login.SingleOrDefault(x => x.Username == Username);
            // Check the password against a list of valid users data
            if (userData!=null)
            {
                if (HashString(Password) == userData.Password) 
                {
                    // Set an authentication cookie or token
                    HttpContext.Session.SetString("Username", Username);
                    return RedirectToAction("Index", "Review");
                }
                ViewBag.ErrorMessage = "Érvénytelen felhasználónév, vagy jelszó!";
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Érvénytelen felhasználónév, vagy jelszó!";
                return View();
            }
        }

        public IActionResult SignOut()
        {
            // Clear the user's session.
            HttpContext.Session.Clear();

            // Redirect the user to the home page.
            return RedirectToAction("Index", "Home");
        }
        public class LoginFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                // Check for the presence of the authentication cookie or token
                if (context.HttpContext.Session.GetString("Username") == null)
                {
                    // Redirect to the login form if not logged in
                    context.Result = new RedirectToActionResult("Login", "Admin", null);
                }
            }
        }
        public class NotLoggedInFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                // Check for the presence of the authentication cookie or token
                if (context.HttpContext.Session.GetString("Username") != null)
                {
                    // Redirect to the login form if not logged in
                    context.Result = new RedirectToActionResult("Index", "Review", null);
                }
            }
        }
    }
}
