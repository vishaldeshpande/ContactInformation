using ContactInformation.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactInformation.DataService.Controllers
{
    [Route("api/Customer")]
    public class CustomerController : ApiController
    {
        IGenericRepository<Customer> customerRepository;
        public CustomerController(IGenericRepository<Customer> repository)
        {
            this.customerRepository = repository;
        }


        [HttpPost]
        [Route("api/Customer/Save")]
        public Customer Save(Customer customer)
        {
            if (!(customer.Id > 0))
            {
                customerRepository.Post(customer);
            }
            else
            {
                customerRepository.Put(customer, customer.Id);
            }
            return customer;
        }

        [HttpGet]
        [Route("api/Customer/Edit")]
        public Customer Edit(int id)
        {
            return customerRepository.Get(id);
        }

        [HttpGet]
        [Route("api/Customer/Delete")]
        public int Delete(int Id)
        {
            Customer customer = customerRepository.Get(Id);
            int result = customerRepository.Remove(customer);
            return result;
        }

        [HttpGet]
        [Route("api/customer/LoadCustomerInfo")]
        public Customer LoadCustomerInfo(int customerId)
        {
            Customer customer = customerRepository.GetSingle(x => x.Id == customerId, x => x.CustomerContacts);
            return customer;
        }

        [HttpGet]
        [Route("api/Customer/GetCustomerList")]
        public List<Customer> GetCustomerList()
        {
            return customerRepository.GetAll();
        }
    }
}
