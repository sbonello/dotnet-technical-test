using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Exceptions;
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
        [ProducesResponseType(200)]
        public void DepositeToAccount(Int32 account, [FromBody] AccountTransfer transferDetails)
        {
            AccountsManager.Deposit(account, transferDetails.Funds);
        }

        // GET api/customers/5
        [HttpPost]
        [Route("api/accounts/{account}/withdraw")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult WithdrawAccount(Int32 account, [FromBody] AccountTransfer transferDetails)
        {
            try
            {
                AccountsManager.Withdraw(account, transferDetails.Funds);
            }
            catch (NotEnoughFundsException)
            {
                return BadRequest("Not enough funds to perform the transfer");
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/accounts/transfer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Transfer([FromBody] AccountTransfer transferDetails)
        {
            if (transferDetails.To == transferDetails.From)
                return BadRequest("Transfer is to the same account");

            try
            {
                AccountsManager.TransferAmount(transferDetails.From,
                                               transferDetails.To,
                                               transferDetails.Funds);
            }
            catch (NotEnoughFundsException)
            {
                return BadRequest("Not enough funds to perform the transfer");
            }

            return Ok();
        }

        #endregion
    }
}