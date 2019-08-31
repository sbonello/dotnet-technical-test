using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalTest.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// The account manager
        /// </summary>
        private readonly IAccountsManager AccountsManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController" /> class.
        /// </summary>
        /// <param name="accountsManager">The accounts manager.</param>
        public AccountsController(IAccountsManager accountsManager)
        {
            this.AccountsManager = accountsManager;
        }

        #endregion

        #region Methods

        // GET api/customers/5
        [HttpPost]
        [Route("api/accounts/{account}/deposit")]
        public void DepositeToAccount(Int32 account, AccountTransfer transferDetails)
        {
            AccountsManager.Deposit(account, transferDetails.Funds);
        }

        // GET api/customers/5
        [HttpPost]
        [Route("api/accounts/{account}/withdraw")]
        public void WithdrawAccount(Int32 account, AccountTransfer transferDetails)
        {
            AccountsManager.Withdraw(account, transferDetails.Funds);
        }

        [HttpPost]
        [Route("api/accounts/transfer")]
        public void Transfer(AccountTransfer transferDetails)
        {
            AccountsManager.TransferAmount(transferDetails.From,
                                           transferDetails.To,
                                           transferDetails.Funds);
        }

        #endregion
    }
}