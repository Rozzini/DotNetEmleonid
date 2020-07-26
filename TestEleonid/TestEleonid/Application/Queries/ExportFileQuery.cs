using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Application.Queries
{
    public class ExportFileQuery : IRequest<MemoryStream>
    {
        public Statuses? Status { get; }

        public Types? Type { get; }

        public ExportFileQuery(Statuses? status, Types? type)
        {
            Status = status;

            Type = type;
        }
    }
}
