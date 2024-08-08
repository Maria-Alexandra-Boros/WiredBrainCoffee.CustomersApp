using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Data
{
    public interface ICustomerDataProvider
    {
        Task<IEnumerable<Customer>?> GetAllAsync();
    }
    public class CustomerDataProvider : ICustomerDataProvider
    {
        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            await Task.Delay(100); ///Simulate a bit of server work
            return new List<Customer>
            {
                new Customer() {Id=1, FirstName="Julia", LastName="Rider"},
                new Customer() {Id=2, FirstName="Maria", LastName="Alexandra", IsDeveloper=true},
                new Customer() {Id=3, FirstName="Betty", LastName="Anca", IsDeveloper=true},
                new Customer() {Id=4, FirstName="Andrei", LastName="Razvan", IsDeveloper=true},
                new Customer() {Id=5, FirstName="Sara", LastName="Clara"},
                new Customer() {Id=6, FirstName="Madalina", LastName="Simion"}
            };
        }
    }
}
