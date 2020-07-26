using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestEleonid.Application.Commands;
using TestEleonid.Repository;

namespace TestEleonid.Application.Handlers
{
    public class EditTransactionHandler : IRequestHandler<EditTransactionCommand>
    {
        private readonly ITransactionRepository _repository;

        public EditTransactionHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
           _repository.EditTransaction(request.TransactionId, request.Status);
            return Unit.Value;
        }
    }
}
