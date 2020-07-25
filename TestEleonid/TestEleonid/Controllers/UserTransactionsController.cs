using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestEleonid;
using TestEleonid.Models;
using TestEleonid.Repository;

namespace TestEleonid.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserTransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _repository;


        public UserTransactionsController(ITransactionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetTransactions()
        {
            return _repository.GetAllTransactions();
        }

        [HttpPost, Route("ImportFile")]
        public async Task<IActionResult> ImportFile(IFormFile file)
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
            string GetTransactionsUrl = "~/api/GetTransactions";
            return Redirect(GetTransactionsUrl);
        }

        [HttpGet, Route("DownloadFile")]
        public IActionResult DownloadFile(string status, string type)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            var csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture);
            List<UserTransaction> a = _repository.GetAllTransactions(status, type);
            csvWriter.WriteRecords(_repository.GetAllTransactions(status, type));
            streamWriter.Flush();
            memoryStream.Position = 0;
            return File(memoryStream, "text/csv", "Transactions.csv");
        }
       
        [HttpPost, Route("EditTransaction")]
        public async Task<IActionResult> EditTransaction(int id, string status)
        {
            _repository.EditTransaction(id, status);
            string GetTransactionsUrl = "~/api/GetTransactions";
            return Redirect(GetTransactionsUrl);
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTransaction>> DeleteUserTransaction(int id)
        {
            _repository.DeleteTransaction(id);

            return NoContent();
        }
    }
}
