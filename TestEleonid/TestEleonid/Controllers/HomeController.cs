using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestEleonid.Models;
using TestEleonid.Repository;

namespace TestEleonid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<HomeController> _logger;


        public HomeController(ITransactionRepository repository, ILogger<HomeController> logger)
        {
            _logger = logger;
            _repository = repository;
        }


        public IActionResult Index()
        {
            return View(_repository.GetAllTransactions());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<UserTransaction>().ToList<UserTransaction>();
                    _repository.AddOrUpdateTransactions(records);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            //using (var memoryStream = new MemoryStream())
            //{
                var memoryStream = new MemoryStream();
                var streamWriter = new StreamWriter(memoryStream);
                var csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture);
                csvWriter.WriteRecords(_repository.GetAllTransactions());
                streamWriter.Flush();
                memoryStream.Position = 0;
                return File(memoryStream, "text/csv", "Transactions.csv");
           // }
           
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
