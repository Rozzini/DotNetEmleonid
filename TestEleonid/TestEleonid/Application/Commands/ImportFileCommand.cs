using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEleonid.Application.Commands
{
    public class ImportFileCommand : IRequest
    {
        public IFormFile File { get; set; }
    }
}
