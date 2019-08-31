using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DataTransferObjects;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repository.DataObject;
using Repository.Exceptions;

namespace Repository
{
    public class MongoRepository : IRepository
    {
        #region Fields

        private IMongoDatabase _database;

        private IMongoCollection<CustomerDO> Customers
        {
            get
            {
                return _database.GetCollection<CustomerDO>("Customer");
            }
        }

        #endregion


        #region Constructor

        public MongoRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        #endregion

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCustomer(int id)
        {
            var filter = Builders<CustomerDO>.Filter.Eq(r => r.ExternalId, id);
            this.Customers.DeleteOne(filter);
        }

        public void DepositFunds(int customerId, decimal funds)
        {
            var filter = Builders<CustomerDO>.Filter.Eq(r => r.ExternalId, customerId);
            CustomerDO customerDO = this.Customers.Find(filter).FirstOrDefault();

            if (customerDO == null)
                throw new AccountNotFound();

            var update = Builders<CustomerDO>.Update.Set(x => x.Balance, customerDO.Balance + funds);
            var result = this.Customers.UpdateOneAsync(filter, update).Result;
        }

        public void WithdrawFunds(int customerId, decimal funds)
        {
            var filter = Builders<CustomerDO>.Filter.Eq(r => r.ExternalId, customerId);
            CustomerDO customerDO = this.Customers.Find(filter).FirstOrDefault();

            if (customerDO == null)
                throw new AccountNotFound();

            var update = Builders<CustomerDO>.Update.Set(x => x.Balance, customerDO.Balance - funds);
            var result = this.Customers.UpdateOneAsync(filter, update).Result;
        }

        public decimal GetAvailableFunds(int customerId)
        {
            var filter = Builders<CustomerDO>.Filter.Eq(r => r.ExternalId, customerId);
            CustomerDO customer = this.Customers.Find(filter).FirstOrDefault();

            if (customer == null)
                throw new AccountNotFound();

            return customer.Balance;
            
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Customer GetCustomer(int id)
        {
            var filter = Builders<CustomerDO>.Filter.Eq(r => r.ExternalId, id);
            CustomerDO customer = this.Customers.Find(filter).FirstOrDefault();

            if (customer == null)
                throw new AccountNotFound();

            return new Customer()
            {
                Id = customer.ExternalId,
                IdCard = customer.IdCard,
                Name = customer.Name,
                Surname = customer.Surname,
            };
        }

        /// <summary>
        /// List of customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> ListCustomers()
        {
            List <CustomerDO> customerDOs = this.Customers.AsQueryable().ToList();

            return new List<Customer>(customerDOs.Select( u => new Customer() { Id = u.ExternalId,
                                                                                IdCard = u.IdCard,
                                                                                Name = u.Name,
                                                                                Surname = u.Surname }));
        }

        /// <summary>
        /// Saves the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SaveCustomer(Customer customer)
        {
            var filter = Builders<CustomerDO>.Filter.Eq(r => r.ExternalId, customer.Id);
            CustomerDO customerDO = this.Customers.Find(filter).FirstOrDefault();

            if (customerDO == null)
            {
                this.Customers.InsertOne(new CustomerDO()
                {
                    ExternalId = customer.Id,
                    IdCard = customer.IdCard,
                    Name = customer.Name,
                    Surname = customer.Surname,
                    Balance = 0,
                });
            }
            else
            {
                customerDO.IdCard = customer.IdCard;
                customerDO.Name = customer.Name;
                customerDO.Surname = customer.Surname;

                this.Customers.ReplaceOne(filter, customerDO);
            }
        }
    }
}
