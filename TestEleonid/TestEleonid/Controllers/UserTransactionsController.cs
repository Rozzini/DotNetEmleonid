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
using TestEleonid.BusinessLogic;
using TestEleonid.Models;
using TestEleonid.Repository;

namespace TestEleonid.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserTransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _repository;


        const string GetTransactionsUrl = "~/api/GetTransactions";

        public UserTransactionsController(ITransactionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetTransactions()
        {
            return _repository.GetAllTransactions().ToList();
        }

        [HttpPost, Route("ImportFile")]
        public async Task<IActionResult> ImportFile(IFormFile file)
        {
            CsvBuilder.ImportHelper(_repository.AddOrUpdateTransactions, file);
            return Redirect(GetTransactionsUrl);
        }

        [HttpGet, Route("DownloadFile")]
        public IActionResult ExportFile(Statuses? status, Types? type)
        {
            return File(CsvBuilder.ExportHelper(_repository.GetAllTransactions, status,type), "text/csv", "Transactions.csv");
        }
       
        [HttpPost, Route("EditTransaction")]
        public async Task<IActionResult> EditTransaction(int id, Statuses? status)
        {
            _repository.EditTransaction(id, status);
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
