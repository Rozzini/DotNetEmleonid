using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Repository
{
    //All repository methods described in Interface
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext appDbContext;

        public TransactionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void DeleteTransaction(int transactionId)
        {
            var transaction = appDbContext.Transactions.FirstOrDefault(item => item.TransactionId == transactionId);
            if (transaction != null)
            {
                appDbContext.Transactions.Remove(transaction);
                appDbContext.SaveChanges();
            }
        }

        public  void EditTransaction(int transactionId, Statuses? status)
        {
            if (status == null) return;
            var transaction = appDbContext.Transactions.FirstOrDefault(item => item.TransactionId == transactionId);

            if (transaction != null)
            {
                transaction.Status = status;
                appDbContext.SaveChanges();
            }
        }

        public List<UserTransaction> GetAllTransactions()
        {
            return appDbContext.Transactions.ToList();
        }

        public List<UserTransaction> GetAllTransactions(Statuses? status, Types? type)
        {
            if(status == null && type == null)
            {
                return appDbContext.Transactions.ToList();
            }
            if(type == null)
            {
                return appDbContext.Transactions.Where(b => b.Status == status).ToList();
            }
            if(status == null)
            {
                return appDbContext.Transactions.Where(b => b.Type == type).ToList();
            }
            else
            {
                return appDbContext.Transactions.Where(b => b.Status == status && b.Type == type).ToList();
            }
        }

        public void AddOrUpdateTransactions(IEnumerable<UserTransaction> transactions)
        {
            foreach (var data in transactions)
            {
                var exists = appDbContext.Transactions.AsNoTracking().Any(item => item.TransactionId == data.TransactionId);
                if (exists)
                {
                    appDbContext.Transactions.Update(data);
                    continue;
                }
                data.TransactionId = 0;
                appDbContext.Transactions.Add(data);
            }
           appDbContext.SaveChanges();
        }
    }
}
