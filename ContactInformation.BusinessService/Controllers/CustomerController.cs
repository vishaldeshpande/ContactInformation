using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestSharp;
using System.Configuration;
using ContactInformation.ReadModel;
using ContactInformation.ReadModel.Shared;

namespace ContactInformation.BusinessService.Controllers
{
    public class CustomerController : ApiController
    {
        RestClient client;
        CIRestProperties restProperties;
        public CustomerController()
        {
            client = new RestClient(ConfigurationManager.AppSettings["DataServiceUrl"].ToString());
            restProperties = new CIRestProperties();
            restProperties.Controller = typeof(Customer).Name;
        }

        [HttpGet]
        [Route("api/Customer/GetCustomerList")]
        public List<Customer> GetCustomerList()
        {
            restProperties.Method = "GetCustomerList";
            restProperties.MethodType = Method.GET;

            IRestResponse<List<Customer>> response = CIRestService.ExecuteRestRequest<List<Customer>>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }

        // GET: api/Customer
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet]
        [Route("api/Customer/EditCustomer")]
        public Customer EditCustomer(int id)
        {
            CIRestProperties restProperties = new CIRestProperties();
            restProperties.Controller = typeof(Customer).Name;
            restProperties.Method = "Edit";
            restProperties.MethodType = Method.GET;
            restProperties.Parameters = new List<CIRestParameter>();
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "id",
                value = id
            });

            IRestResponse<Customer> response = CIRestService.ExecuteRestRequest<Customer>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(response.Content);
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }


        [HttpGet]
        [Route("api/Customer/GetCustomerwithContacts")]
        public Customer GetCustomerwithContacts(int id)
        {
            ContactTypeController contactTypeController = new Controllers.ContactTypeController();
            List<ContactType> contactTypes = contactTypeController.GetContactTypes();

            CIRestProperties restProperties = new CIRestProperties();
            restProperties.Controller = typeof(Customer).Name;
            restProperties.Method = "LoadCustomerInfo";
            restProperties.MethodType = Method.GET;
            restProperties.Parameters = new List<CIRestParameter>();
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "customerId",
                value = id
            });

            IRestResponse<Customer> response = CIRestService.ExecuteRestRequest<Customer>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(response.Content);

                foreach (CustomerContact contact in customer.CustomerContacts)
                {
                    contact.ContactType = contactTypes.FirstOrDefault(x => x.Id == contact.ContactTypeId);
                }

                return customer;
            }

            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }

        // POST: api/Customer
        //public void Post([FromBody]string value)
        //{
        //}

        [HttpPost]
        [Route("api/Customer/SaveCustomer")]
        public Customer SaveCustomer(Customer customer)
        {
            CIRestProperties restProperties = new CIRestProperties();
            restProperties.Controller = typeof(Customer).Name;
            restProperties.Method = "Save";
            restProperties.MethodType = Method.POST;
            //restProperties.Body = customer;
            restProperties.Parameters = new List<CIRestParameter>();
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "customer",
                value = customer
            });

            IRestResponse<Customer> response = CIRestService.ExecuteRestRequest<Customer>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(response.Content);
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpGet]
        [Route("api/Customer/DeleteCustomer")]
        // DELETE: api/Customer/5
        public bool DeleteCustomer(int id)
        {
            restProperties.Method = "Delete";
            restProperties.MethodType = Method.GET;
            restProperties.Parameters = new List<CIRestParameter>();
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "id",
                value = id
            });

            IRestResponse<int> response = CIRestService.ExecuteRestRequest<int>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data > 0)
                    return true;
                else
                    return false;
            }
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }
    }
}
