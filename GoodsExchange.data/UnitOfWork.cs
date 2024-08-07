﻿using GoodsExchange.data.Models;
using GoodsExchange.data.Repository;

namespace GoodsExchange.data
{
    public class UnitOfWork
    {
        private Net1710_221_7_GoodsExchangeContext _context;
        private PostRepository _postRepository;
        private CategoryRepository _categoryRepository;
        private OfferRepository _offerRepository;
        private CommentRepository _commentRepository;
        private OfferDetailRepository _offerDetailRepository;
        public UnitOfWork(Net1710_221_7_GoodsExchangeContext context)
        {
            _context = context;
        }
        public UnitOfWork()
        {
            _context ??= new Net1710_221_7_GoodsExchangeContext();
        }
        private CustomerRepository _customerRepository;
        public OfferRepository OfferRepository
        {
            get { return _offerRepository ??= new OfferRepository(); }
        }
        public PostRepository PostRepository
        {
            get { return _postRepository ??= new PostRepository(_context); }
        }
        public CategoryRepository CategoryRepository
        {
            get { return _categoryRepository ??= new CategoryRepository(); }
        }

        public CustomerRepository CustomerRepository
        {
            get { return _customerRepository ??= new CustomerRepository(); }
        }

        public CommentRepository CommentRepository
        {
            get { return _commentRepository ??= new CommentRepository(_context); }
        }
        public OfferDetailRepository OfferDetailRepository
        {
            get { return _offerDetailRepository ??= new OfferDetailRepository(); }
        }


        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}
