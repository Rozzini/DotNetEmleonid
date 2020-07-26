using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestEleonid;
using TestEleonid.Application.Commands;
using TestEleonid.Application.Queries;
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
        private readonly IMediator _mediator;

        const string GetTransactionsUrl = "~/api/GetTransactions";

        public UserTransactionsController(ITransactionRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetTransactions()
        {
            var query = new GetTransactionsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsByParameters(Statuses? status, Types? type)
        {
            var query = new GetTransactionsByParametersQuery(status, type);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ImportFile(IFormFile file)
        {
            ImportFileCommand command = new ImportFileCommand();
            command.File = file;
            var result = await _mediator.Send(command);
            return Redirect(GetTransactionsUrl);
        }

        [HttpGet]
        public async Task<IActionResult> ExportFile(Statuses? status, Types? type)
        {
            var query = new ExportFileQuery(status, type);
            var result = await _mediator.Send(query);
            return File(result, "text/csv", "Transactions.csv");
        }
        

        [HttpPost]
        public async Task<IActionResult> EditTransaction(int id, Statuses? status)
        {
            EditTransactionCommand command = new EditTransactionCommand();
            command.TransactionId = id;
            command.Status = status;
            var result = await _mediator.Send(command);
            return Redirect(GetTransactionsUrl);
        }
        

        [HttpPost]
        public async Task<IActionResult> DeleteUserTransaction(int id)
        {
            DeleteUserTransactionCommand command = new DeleteUserTransactionCommand();
            command.TransactionId = id;
            var result = await _mediator.Send(command);
            return NoContent();
        }
    }
}
