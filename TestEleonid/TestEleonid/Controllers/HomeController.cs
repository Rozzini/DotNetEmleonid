using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestEleonid.Models;

namespace TestEleonid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //if (file == null || file.Length == 0)
        //    return Content("file not selected");

        //var path = Path.Combine(
        //            Directory.GetCurrentDirectory(), "wwwroot",
        //            file.FileName);

        //using (var stream = new FileStream(path, FileMode.Create))
        //{
        //    try
        //    {
        //        fileHelper.ConvertToPDF(stream, file.ContentType, file.FileName, targetLocation);
        //        await file.CopyToAsync(stream);
        //    }
        //    catch
        //    {
        //        stream.Dispose();
        //        throw;
        //    }
        //}
        //return RedirectToAction("Files");
        //}

        //@"C:\temp\data.csv"
        public IActionResult Index()
        {
            using (var reader = new StreamReader(@"C:\temp\data.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<UserTransaction>().ToList<UserTransaction>();
            }
            return View();
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
