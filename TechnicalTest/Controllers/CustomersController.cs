namespace TechnicalTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic;
    using DataTransferObjects;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// The customers manager
        /// </summary>
        private readonly ICustomersManager CustomersManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController" /> class.
        /// </summary>
        /// <param name="customersManager">The customers manager.</param>
        public CustomersController(ICustomersManager customersManager)
        {
            this.CustomersManager = customersManager;
        }

        #endregion

        #region Methods

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public void Delete(Int32 id)
        {
            this.CustomersManager.DeleteCustomer(id);
        }

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return this.Ok(this.CustomersManager.ListCustomers());
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public ActionResult<String> Get(Int32 id)
        {
            Customer customer = this.CustomersManager.GetCustomer(id);
            return this.Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            this.CustomersManager.SaveCustomer(customer);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public void Put([FromBody] Customer customer)
        {
            this.CustomersManager.SaveCustomer(customer);
        }

        #endregion
    }
}