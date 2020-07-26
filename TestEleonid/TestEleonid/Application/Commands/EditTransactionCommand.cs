using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Application.Commands
{
    public class EditTransactionCommand : IRequest
    {
        public int TransactionId { get; set; }

        public Statuses? Status { get; set; }
    }
}
