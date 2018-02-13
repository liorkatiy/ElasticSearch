using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ElasticSearchApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CustomersController : ApiController
    {
        
        static CustomersCRUD customerDB = new CustomersCRUD();

        // GET: api/Customers
        public async Task<IEnumerable<Customer>> Get()
        {
            var customers = await customerDB.Get();
            return customers;
        }

        // GET: api/Customers/5
        public async Task<IEnumerable<Customer>> Get(string id)
        {
            var customers = await customerDB.Get(id);
            return customers;
        }

        // POST: api/Customers
        public async Task<string> Post([FromBody]Customer customer)
        {
            var id = await customerDB.Create(customer);
            return id;
        }

        // PUT: api/Customers/5
        public async Task<bool> Put(string id, [FromBody]Customer customer)
        {
            var updated = await customerDB.Update(id, customer);
            return updated;
        }

        // DELETE: api/Customers/5
        public async Task<bool> Delete(string id)
        {
            bool deleted = await customerDB.Delete(id);
            return deleted;
        }
    }
}
