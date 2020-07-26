using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestEleonid.Application.Commands;
using TestEleonid.Repository;

namespace TestEleonid.Application.Handlers
{
    public class DeleteUserTransactionHandler : IRequestHandler<DeleteUserTransactionCommand>
    {
        private readonly ITransactionRepository _repository;

        public DeleteUserTransactionHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteUserTransactionCommand request, CancellationToken cancellationToken)
        {
            _repository.DeleteTransaction(request.TransactionId);
            return Unit.Value;
        }
    }
}
