using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEleonid.Application.Commands
{
    public class DeleteUserTransactionCommand : IRequest
    {
        public int TransactionId { get; set; }
    }
}
