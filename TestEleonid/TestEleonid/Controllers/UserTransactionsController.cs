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
    [Route("api/[controller]")]
    [ApiController]
    public class UserTransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<HomeController> _logger;


        public UserTransactionsController(ITransactionRepository repository, ILogger<HomeController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/UserTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetTransactions(IFormFile file)
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

            return _repository.GetAllTransactions();
        }


        //public IActionResult DownloadFiles(string subDirectory)
        //{
        //    try
        //    {
        //        var (fileType, archiveData, archiveName) = _fileService.FetechFiles(subDirectory);

        //        return File(archiveData, fileType, archiveName);
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest($"Error: {exception.Message}");
        //    }
        //}

        // GET: api/UserTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTransaction>> GetUserTransaction(int id)
        {
            //var userTransaction = await _context.Transactions.FindAsync(id);

            //if (userTransaction == null)
            //{
            //    return NotFound();
            //}

            //return userTransaction;
            return NoContent();
        }

        // PUT: api/UserTransactions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTransaction(int id, UserTransaction userTransaction)
        {
            //if (id != userTransaction.TransactionId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(userTransaction).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserTransactionExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/UserTransactions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserTransaction>> PostUserTransaction(UserTransaction userTransaction)
        {
            //_context.Transactions.Add(userTransaction);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTransaction", new { id = userTransaction.TransactionId }, userTransaction);
        }

        // DELETE: api/UserTransactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTransaction>> DeleteUserTransaction(int id)
        {
            //var userTransaction = await _context.Transactions.FindAsync(id);
            //if (userTransaction == null)
            //{
            //    return NotFound();
            //}

            //_context.Transactions.Remove(userTransaction);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool UserTransactionExists(int id)
        //{
        //    return null;//_context.Transactions.Any(e => e.TransactionId == id);
        //}
    }
}
