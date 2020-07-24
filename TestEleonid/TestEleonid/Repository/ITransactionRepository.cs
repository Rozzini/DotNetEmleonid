using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Repository
{
    public interface ITransactionRepository
    {
        List<UserTransaction> GetAllGetTransactions();

        void DeleteTransaction(int TransactionId);

        void EditTransaction(int TransactionId, string status);

        void UpLoadTransaction(IEnumerable<UserTransaction> records);
    }
}
