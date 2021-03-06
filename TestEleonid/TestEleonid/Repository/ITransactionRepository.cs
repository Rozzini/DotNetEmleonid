﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.Repository
{
    public interface ITransactionRepository
    {
        //Returns list of all entities from DB
        IEnumerable<UserTransaction> GetAllTransactions();

        //Return list of all entities from DB depending on parameters 
        IEnumerable<UserTransaction> GetTransactionsByParameters(Statuses? status, Types? type);

        //Deletes selected entity
        void DeleteTransaction(int transactionId);

        //Edit entities status with given id 
        void EditTransaction(int transactionId, Statuses? status);

        //If entity with given id exists in DB it will be updated else it will be added to DB
        void AddOrUpdateTransactions(IEnumerable<UserTransaction> records);
    }
}
