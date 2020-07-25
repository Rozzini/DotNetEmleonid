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

        public async void DeleteTransaction(int transactionId)
        {
            var transaction = appDbContext.Transactions.FirstOrDefaultAsync(item => item.TransactionId == transactionId);
            if (transaction != null)
            {
                appDbContext.Transactions.Remove(transaction.Result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async void EditTransaction(int transactionId, string status)
        {
            if (status == null) return;
            var transaction = appDbContext.Transactions.FirstOrDefaultAsync(item => item.TransactionId == transactionId);

            if (transaction != null)
            {
                transaction.Result.Status = status;
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<UserTransaction>> GetAllTransactions()
        {
            return await appDbContext.Transactions.ToListAsync();
        }

        public async Task<List<UserTransaction>> GetAllTransactions(string status, string type)
        {
            if(status == null && type == null)
            {
                return await appDbContext.Transactions.ToListAsync();
            }
            if(type == null)
            {
                return await appDbContext.Transactions.Where(b => b.Status == status).ToListAsync();
            }
            if(status == null)
            {
                return await appDbContext.Transactions.Where(b => b.Type == type).ToListAsync();
            }
            else
            {
                return await appDbContext.Transactions.Where(b => b.Status == status && b.Type == type).ToListAsync();
            }
        }

        public async void AddOrUpdateTransactions(IEnumerable<UserTransaction> transactions)
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
            await appDbContext.SaveChangesAsync();
        }
    }
}
