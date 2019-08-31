using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    /// <summary>
    /// DTO to represent account transfers
    /// </summary>
    public class AccountTransfer
    {
        #region Fields
        /// <summary>
        /// Gets or sets the From Account
        /// </summary>

        /// <value>
        /// Account from which funds are transfered
        /// </value>
        public Int32 From { get; set; }

        /// <summary>
        /// Gets or sets the To Account
        /// </summary>

        /// <value>
        /// Account to which funds are transfered
        /// </value>

        public Int32 To { get; set; }

        /// <summary>
        /// Gets or sets the To Amount
        /// </summary>

        /// <value>
        /// Amount to transfer  
        /// </value>
        public Decimal Funds { get; set; }

        #endregion
    }
}
