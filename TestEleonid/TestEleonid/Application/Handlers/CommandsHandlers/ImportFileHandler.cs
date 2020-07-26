using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestEleonid.Application.Commands;
using TestEleonid.BusinessLogic;
using TestEleonid.Repository;

namespace TestEleonid.Application.Handlers.CommandsHandlers
{
    public class ImportFileHandler : IRequestHandler<ImportFileCommand>
    {
        private readonly ITransactionRepository _repository;

        public ImportFileHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ImportFileCommand request, CancellationToken cancellationToken)
        {
            CsvBuilder.ImportHelper(_repository.AddOrUpdateTransactions, request.File);
            return Unit.Value;
        }
    }
}
