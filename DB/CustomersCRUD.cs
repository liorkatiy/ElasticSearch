using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace DB
{
    public class CustomersCRUD
    {
        ElasticClient client;

        public CustomersCRUD()
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("customers");
            client = new ElasticClient(settings);
        }

        public async Task<IEnumerable<Customer>> Get(string search)
        {
            search = "*" + search + "*";
            var result = await client.SearchAsync<Customer>(
                s => s.Query(
                    q =>
                        q.Wildcard(
                            m => m.Field(
                                f => f.IdCard).Value(search)) ||
                        q.Wildcard(
                            m => m.Field(
                                f => f.FirstName).Value(search)) ||
                        q.Wildcard(
                            m => m.Field(
                                f => f.LastName).Value(search))
                    ).Size(50));
            return result.Documents;
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            return await Get(null);
        }

        public async Task<ID> Create(Customer c)
        {
            c.Id = Guid.NewGuid();
            var result = await client.IndexDocumentAsync(c);
            return new ID(result.Id);
        }

        public async Task<bool> Update(string ID, Customer newCustomer)
        {
            var result = await client.
                UpdateAsync(new DocumentPath<Customer>(ID),
                c => c.Doc(newCustomer));
            return result.IsValid;
        }

        public async Task<bool> Delete(string ID)
        {
            var result = await client.
                DeleteAsync(new DocumentPath<Customer>(ID));
            return result.IsValid;
        }
    }
}