using Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class AccountsManager : IAccountsManager
    {
        #region Fields

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository Repository;

        private static ConcurrentDictionary<Int32, object> LockObjects = new ConcurrentDictionary<Int32, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountsManager(IRepository repository)
        {
            this.Repository = repository;
        }

        #endregion

        /// <summary>
        /// Deposit funds to account
        /// </summary>
        /// <param name="customerid">Customer Account Indentifier</param>
        /// <param name="funds">Amount of funds to be transfered</param>
        public void Deposit(Int32 customerid, decimal funds)
        {
            var lockObject = LockObjects.GetOrAdd(customerid, new object());
            lock (lockObject)
            {
                try
                {
                    this.Repository.DepositFunds(customerid, funds);
                }
                finally
                {
                    LockObjects.TryRemove(customerid, out _);
                }
            }
        }

        /// <summary>
        /// Withdraw funds from account
        /// </summary>
        /// <param name="customerid">Customer Account Identifier</param>
        /// <param name="funds">Amount of funds to be transfered</param>
        public void Withdraw(Int32 customerid, decimal funds)
        {
            var lockObject = LockObjects.GetOrAdd(customerid, new object());
            lock (lockObject)
            {
                try
                {
                    this.Repository.WithdrawFunds(customerid, funds);
                }
                finally
                {
                    LockObjects.TryRemove(customerid, out _);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromCustomerId">Account from which the funds need to be transferd</param>
        /// <param name="toCustomerId">Account to tranfer funds</param>
        /// <param name="funds">Amount of funds to be transfered</param>
        public void TransferAmount(Int32 fromCustomerId, int toCustomerId, decimal funds)
        {
            var fromlockObject = LockObjects.GetOrAdd(fromCustomerId, new object());
            var tolockObject = LockObjects.GetOrAdd(toCustomerId, new object());

            lock (fromlockObject) lock(tolockObject)
            {
                try
                {
                    this.Repository.WithdrawFunds(fromCustomerId, funds);
                    this.Repository.DepositFunds(toCustomerId, funds);
                }
                finally
                {
                    LockObjects.TryRemove(fromCustomerId, out _);
                    LockObjects.TryRemove(toCustomerId, out _);
                }
            }
        }
    }
}
