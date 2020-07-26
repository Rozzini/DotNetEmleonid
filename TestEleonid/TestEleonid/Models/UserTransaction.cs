using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestEleonid.Models
{
    public enum Statuses
    {
        Pending,
        Completed,
        Cancelled
    }
    public enum Types
    {
        Refill,
        Withdrawal
    }

    public class UserTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public Statuses? Status { get; set; }

        public Types? Type { get; set; }

        public string ClientName { get; set; }

        public string Amount { get; set; }
    }
}
