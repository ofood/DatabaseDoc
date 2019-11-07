using DatabaseDoc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseDoc.Core
{
    public class EFDbContext :DbContext,IDbContext
    {
        
        public EFDbContext(DbContextOptions<EFDbContext> options): base(options)
        {
            base.ChangeTracker.LazyLoadingEnabled = false;
        }
        //public DbSet<QuestionType> QuestionTypes { get; set; }
        //public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDbContext).Assembly);
        }
        public bool IsTransactionStarted { get; private set; }

        public void BeginTransaction()
        {
            if (IsTransactionStarted)
                throw new InvalidOperationException("Transaction is already started.");

            this.Database.BeginTransaction();
            IsTransactionStarted = true;
        }

        public void Commit()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            this.Database.CurrentTransaction.Commit();

            IsTransactionStarted = false;
        }

        public void Rollback()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");
            this.Database.RollbackTransaction();
            IsTransactionStarted = false;
        }
    }
}
