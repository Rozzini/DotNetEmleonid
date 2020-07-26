using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Application.Queries
{
    public class GetTransactionsQuery : IRequest<IEnumerable<UserTransaction>>
    {
    }
}
