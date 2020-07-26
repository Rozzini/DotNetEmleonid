using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestEleonid.Application.Queries;
using TestEleonid.BusinessLogic;
using TestEleonid.Repository;

namespace TestEleonid.Application.Handlers.QueriesHandlers
{
    public class ExportFileHandler : IRequestHandler<ExportFileQuery, MemoryStream>
    {
        private readonly ITransactionRepository _repository;

        public ExportFileHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<MemoryStream> Handle(ExportFileQuery request, CancellationToken cancellationToken)
        {
            return CsvBuilder.ExportHelper(_repository.GetTransactionsByParameters, request.Status, request.Type);
        }
    }
}
