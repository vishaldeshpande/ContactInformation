using ContactInformation.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactInformation.DataService.Controllers
{
    public class CustomerContactController : ApiController
    {
        IGenericRepository<CustomerContact> customerContactRepository;
        public CustomerContactController(IGenericRepository<CustomerContact> repository)
        {
            this.customerContactRepository = repository;
        }

        [HttpPost]
        [Route("api/CustomerContact/Save")]
        public CustomerContact Save(CustomerContact customerContact)
        {
            if (customerContact.Id > 0)
            {
                customerContactRepository.Put(customerContact, customerContact.Id);
            }
            else
            {
                customerContactRepository.Post(customerContact);
            }

            return customerContact;
        }

        //[HttpGet]
        //[Route("api/CustomerContact/GetContactsOfCustomer")]
        //public List<CustomerContact> GetContactsOfCustomer(int customerId)
        //{
        //    //List<CustomerContact> customerContactList = customerContactRepository.Filter(x => x.CustomerId == customerId)?.ToList();
        //    List<CustomerContact> customerContactList = customerContactRepository.GetSingle(x => x.CustomerId == customerId, );

        //    return customerContactList;
        //}

        [HttpGet]
        [Route("api/CustomerContact/GetContactInformation")]
        public CustomerContact GetContactInformation(int id)
        {
            CustomerContact customerContact = customerContactRepository.GetSingle(x => x.Id == id,
                x=>x.Customer, y=>y.ContactType);

            return customerContact;
        }


        [HttpGet]
        [Route("api/CustomerContact/Edit")]
        public CustomerContact Edit(int id)
        {
            return customerContactRepository.Get(id);
        }

        [HttpGet]
        [Route("api/CustomerContact/Delete")]
        public int Delete(int id)
        {
            CustomerContact customerContact = customerContactRepository.Get(id);
            return customerContactRepository.Remove(customerContact);
        }
    }
}