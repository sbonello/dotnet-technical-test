using Audit;
using BusinessLogic;
using BusinessLogic.Exceptions;
using Moq;
using Repository;
using System;
using Xunit;

namespace XUnitTests
{
    public class UnitTestAccountBalance
    {
        [Fact]
        public void TestWithdraw_NotEnoughFundsException()
        {
            var repository = new Mock<IRepository>();
            var transitionalAudit = new Mock<ITransactionAudit>();

            repository.Setup(x => x.GetAvailableFunds(1)).Returns(0);

            AccountsManager accountManager = new AccountsManager(repository.Object,
                                                                 transitionalAudit.Object);

            Assert.Throws<NotEnoughFundsException>( () => accountManager.Withdraw(1, 50));
        }

        [Fact]
        public void TestTransfer_NotEnoughFundsException()
        {
            var repository = new Mock<IRepository>();
            var transitionalAudit = new Mock<ITransactionAudit>();

            repository.Setup(x => x.GetAvailableFunds(1)).Returns(0);

            AccountsManager accountManager = new AccountsManager(repository.Object,
                                                                 transitionalAudit.Object);

            Assert.Throws<NotEnoughFundsException>(() => accountManager.TransferAmount(1, 2, 50));
        }


        [Fact]
        public void TestAccountNotFound_NotEnoughFundsException()
        {
            var repository = new Mock<IRepository>();
            var transitionalAudit = new Mock<ITransactionAudit>();

            repository.Setup(x => x.GetAvailableFunds(1)).Returns(0);

            AccountsManager accountManager = new AccountsManager(repository.Object,
                                                                 transitionalAudit.Object);

            Assert.Throws<NotEnoughFundsException>(() => accountManager.TransferAmount(1, 2, 50));
        }


        [Fact]
        public void TestWithdraw()
        {
            var repository = new Mock<IRepository>();
            var transitionalAudit = new Mock<ITransactionAudit>();

            repository.Setup(x => x.GetAvailableFunds(1)).Returns(100);

            AccountsManager accountManager = new AccountsManager(repository.Object,
                                                                 transitionalAudit.Object);

            accountManager.Withdraw(1, 50);
        }

        [Fact]
        public void TestTransfer()
        {
            var repository = new Mock<IRepository>();
            var transitionalAudit = new Mock<ITransactionAudit>();


            repository.Setup(x => x.GetAvailableFunds(1)).Returns(100);


            AccountsManager accountManager = new AccountsManager(repository.Object,
                                                                 transitionalAudit.Object);

            accountManager.TransferAmount(1, 2, 50);
        }


        [Fact]
        public void TestAccountNotFound()
        {
            var repository = new Mock<IRepository>();
            var transitionalAudit = new Mock<ITransactionAudit>();

            repository.Setup(x => x.GetAvailableFunds(1)).Returns(100);

            AccountsManager accountManager = new AccountsManager(repository.Object,
                                                                 transitionalAudit.Object);

            accountManager.TransferAmount(1, 2, 50);
        }
    }
}

