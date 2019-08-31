using EventStore.ClientAPI;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Audit
{
    public enum TransactionType
    {
        Debit,
        Credit
    };

    public class TransactionAudit : ITransactionAudit
    {
        #region Attributes
        private static string STREAM = "ACCOUNTTRANSACTIONS";
        private IEventStoreConnection conn;
        #endregion

        #region Constructor
        public void AccountTransaction(IOptions<EventStoreSettings> settings)
        {
            var consettings = ConnectionSettings.Create();
            conn = EventStoreConnection.Create(consettings, new IPEndPoint(IPAddress.Parse(settings.Value.IPAddress), 
                                                                                           settings.Value.Port));
            conn.ConnectAsync().Wait();
        }
        #endregion


        #region Methods
        public void AccountTransaction(int accountId, 
                                       decimal amount,
                                       TransactionType type)
        {

            if (type == TransactionType.Credit)
            {
                conn.AppendToStreamAsync(STREAM,
                       ExpectedVersion.Any,
                       new EventData(
                        Guid.NewGuid(),
                        "CREDIT",
                        true,
                        Encoding.UTF8.GetBytes("{ amount:" + amount.ToString() + "}"),
                        new byte[] { }
                        )).Wait();
            }
            else
            {
                conn.AppendToStreamAsync(STREAM,
                       ExpectedVersion.Any,
                       new EventData(
                        Guid.NewGuid(),
                        "DEBIT",
                        true,
                        Encoding.UTF8.GetBytes("{ amount:" + amount.ToString() + "}"),
                        new byte[] { }
                        )).Wait();
            }
        }
        #endregion
    }
}
