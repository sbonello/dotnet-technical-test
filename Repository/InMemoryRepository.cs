using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    using System.Linq;
    using DataTransferObjects;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Repository.IRepository" />
    public class InMemoryRepository : IRepository
    {
        /// <summary>
        /// The customers
        /// </summary>
        private readonly List<Customer> Customers;

        /// <summary>
        /// The balances
        /// </summary>
        private readonly Dictionary<Int32, Decimal> Balances;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository"/> class.
        /// </summary>
        public InMemoryRepository()
        {
            this.Customers = new List<Customer>();
            this.Balances = new Dictionary<Int32, Decimal>();
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCustomer(Int32 id)
        {
            Customer customer = this.GetCustomer(id);
            this.Customers.Remove(customer);
        }

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DepositFunds(Int32 customerId,
                                 Decimal funds)
        {
            if (this.Balances.ContainsKey(customerId))
            {
                this.Balances[customerId] += funds;
            }
            else
            {
                this.Balances.Add(customerId, funds);
            }
        }

        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public Decimal GetAvailableFunds(Int32 customerId)
        {
            return this.Balances.ContainsKey(customerId) ? this.Balances[customerId] : 0;
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Customer GetCustomer(Int32 id)
        {
            return this.Customers.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// List of customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> ListCustomers()
        {
            return this.Customers;
        }

        /// <summary>
        /// Saves the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SaveCustomer(Customer customer)
        {
            if (this.DoesCustomerExist(customer.Id))
            {
                Customer existingCustomer = this.GetCustomer(customer.Id);
                existingCustomer.IdCard = customer.IdCard;
                existingCustomer.Name = customer.Name;
                existingCustomer.Surname = customer.Surname;
            }
            else
            {
                this.Customers.Add(customer);
            }
        }

        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WithdrawFunds(Int32 customerId,
                                  Decimal funds)
        {
            if (this.Balances.ContainsKey(customerId))
            {
                this.Balances[customerId] -= funds;
            }
        }

        /// <summary>
        /// Does the customer exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private Boolean DoesCustomerExist(Int32 id)
        {
            return this.Customers.Any(c => c.Id == id);
        }
    }
}
