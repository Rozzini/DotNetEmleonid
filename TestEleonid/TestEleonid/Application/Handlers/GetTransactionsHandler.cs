using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestEleonid.Application.Queries;
using TestEleonid.Models;
using TestEleonid.Repository;

namespace TestEleonid.Application.Handlers
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, IEnumerable<UserTransaction>>
    {

        private readonly ITransactionRepository _repository;

        public GetTransactionsHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserTransaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllTransactions();
        }
    }
}
