using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEleonid.Models
{
    public class UserTransaction
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public string ClientName { get; set; }

        public double Amount { get; set; }
    }
}
