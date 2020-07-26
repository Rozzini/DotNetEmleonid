using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Application.Queries
{
    public class GetTransactionsByParametersQuery : IRequest<IEnumerable<UserTransaction>>
    {
        public Statuses? Status { get;}

        public Types? Type { get;}

        public GetTransactionsByParametersQuery(Statuses? status, Types? type)
        {
            Status = status;

            Type = type;
        }
    }
}
