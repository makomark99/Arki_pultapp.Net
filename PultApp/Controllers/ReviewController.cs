using Microsoft.AspNetCore.Mvc;
using PultApp.Data;
using System.Linq;
using PultApp.Models;
using OfficeOpenXml;
using static PultApp.Controllers.AdminController;

namespace PultApp.Controllers
{
    [LoginFilter]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ReviewController(ApplicationDbContext db)
        {
            _db = db;
        }
    
        public IActionResult Index()
        {
            IEnumerable<Review> objReviewList = _db.Reviews;
            return View(objReviewList);
        }
        
        public IActionResult Charts()
        {
            var query = from r in _db.Reviews
                        group r by r.Heard into g
                        select new ReviewChartResult
                        {
                            Heard = g.Key,
                            HeardCount = g.Count()
                        };
            var query2 = from r in _db.Reviews
                         where r.Rating != 0 && r.Rating >= 1 && r.Rating <= 4
                         group r by r.Rating into g
                         select new ReviewChartResult
                         {
                             Rating = g.Key,
                             RatingCount = g.Count()
                         };

            var ratings = Enumerable.Range(1, 4);

            var results = from rating in ratings
                          join review in query2 on rating equals review.Rating into grp
                          from g in grp.DefaultIfEmpty()
                          select new ReviewChartResult
                          {
                              Rating = rating,
                              RatingCount = g?.RatingCount ?? 0
                          };


            var HeardList = query.ToList();
            var RatingList = results.ToList();
            HeardList.AddRange(RatingList);
            return View(HeardList);
        }


        public FileContentResult DownloadCsv()
        {
            var data = (from r in _db.Reviews
                        select new Review
                        {
                            Email = r.Email,
                            IsSubscribed = r.IsSubscribed,
                            Heard = r.Heard,
                            Rating = r.Rating,
                            CreatedAt = r.CreatedAt,
                        }).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Write the first row with the column names
                worksheet.Cells[1, 1].Value = "Feliratkozott E-mail";
                worksheet.Cells[1, 2].Value = "Feliratkozva?";
                worksheet.Cells[1, 3].Value = "Honnan hallott rólunk?";
                worksheet.Cells[1, 4].Value = "Értékelés";
                worksheet.Cells[1, 5].Value = "Bejegyezve ekkor";

                // Iterate through the data and add a new row for each record
                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = data[i].Email;
                    string isSubscribed;

                    switch (data[i].IsSubscribed)
                    {
                        case 1:
                            isSubscribed = "Nem, de feliratkozott.";
                            break;
                        case 2:
                            isSubscribed = "Nem és nem is szeretne.";
                            break;
                        case 3:
                            isSubscribed = "Igen, már korábban.";
                            break;
                        default:
                            isSubscribed = "Érvénytelen érték";
                            break;
                    }
                    worksheet.Cells[i + 2, 2].Value = isSubscribed;
                    string Where;

                    switch (data[i].Heard)
                    {
                        case 1:
                            Where = "Régi ügyfél vagyok (több mint 1 éve ismerem a céget.";
                            break;
                        case 2:
                            Where = "Új ügyfél vagyok, és ajánlás útján hallottam a cégről.";
                            break;
                        case 3:
                            Where = "Új ügyfél vagyok, interneten találkoztam a céggel.";
                            break;
                        default:
                            Where = "Érvénytelen érték";
                            break;
                    }
                    worksheet.Cells[i + 2, 3].Value = Where;
                    worksheet.Cells[i + 2, 4].Value = data[i].Rating;
                    worksheet.Cells[i + 2, 5].Value = data[i].CreatedAt.ToString();
                }

                // Save the workbook to a file and return it as a download
                var fileContents = package.GetAsByteArray();
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReviewResults"+DateTime.Now.ToString("yyyyMMddHHmm")+".xlsx");
            }
        }



    }
}
