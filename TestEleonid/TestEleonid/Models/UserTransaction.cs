using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestEleonid.Models
{
    public class UserTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public string ClientName { get; set; }

        public string Amount { get; set; }
    }
}
