using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext appDbContext;

        public TransactionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void DeleteTransaction(int TransactionId)
        {
            var transaction = appDbContext.Transactions.FirstOrDefault(item => item.TransactionId == TransactionId);
            appDbContext.Transactions.Remove(transaction);
            appDbContext.SaveChanges();
        }

        public void EditTransaction(int TransactionId, string status)
        {
            var transaction = appDbContext.Transactions.FirstOrDefault(item => item.TransactionId == TransactionId);

            if (transaction != null)
            {

                transaction.Status = status;
                appDbContext.SaveChanges();
            }
        }

        public List<UserTransaction> GetAllGetTransactions()
        {
            return appDbContext.Transactions.ToList();
        }

        public void UpLoadTransaction(IEnumerable<UserTransaction> records)
        {
            foreach (var data in records)
            {
                var exists = appDbContext.Transactions.AsNoTracking().Any(item => item.TransactionId == data.TransactionId);
                if (exists)
                {
                    appDbContext.Transactions.Update(data);
                    continue;
                }
                appDbContext.Transactions.Add(data);
            }
            appDbContext.SaveChanges();
        }
    }
}
