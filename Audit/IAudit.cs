using System;

namespace Audit
{
    public interface ITransactionAudit
    {
        #region Methods

        void AccountTransaction(Int32 accountId, 
                                decimal amount,
                                TransactionType type);
        #endregion
    }
}
