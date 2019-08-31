using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IAccountsManager
    {
        #region Methods

        /// <summary>
        /// Deposite Amount in Account Balance
        /// </summary>
        /// <param name="amount">Amount to deposit</param>
        void Deposit(Int32 account, Decimal amount);

        /// <summary>
        /// Withdraw Amount in Account Balance
        /// </summary>
        /// <param name="amount">Amount to withdraw</param>
        void Withdraw(Int32 account, Decimal amount);

        /// <summary>
        /// Transfer amount between accounts
        /// </summary>
        /// <param name="from">Account from which funds are transfered</param>
        /// <param name="to">Account receiving funds</param>
        /// <param name="amount">Amount to transfer</param>
        void TransferAmount(Int32 from, Int32 to, Decimal amount);

        #endregion
    }
}
