using ContactInformation.ReadModel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactInformation.MVC.Models;
using ContactInformation.ReadModel.Shared;
using Newtonsoft.Json;

namespace ContactInformation.MVC.Controllers
{
    public class CustomerController : Controller
    {
        RestClient client;
        CIRestProperties restProperties;

        public CustomerController()
        {
            client = new RestClient(ConfigurationManager.AppSettings["BusinessServiceUrl"].ToString());
            restProperties = new CIRestProperties();
            restProperties.Controller = typeof(Customer).Name;
        }

        // GET: Customer
        public ActionResult Index()
        {
            List<CustomerViewModel> customerVMList = new List<CustomerViewModel>();
            restProperties.Method = "GetCustomerList";
            restProperties.MethodType = Method.GET;

            IRestResponse<List<Customer>> response = CIRestService.ExecuteRestRequest<List<Customer>>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                foreach (Customer customer in response.Data)
                {
                    CustomerViewModel customerVM = new CustomerViewModel();
                    customerVM.CustomerId = customer.Id;
                    customerVM.FirstName = customer.FirstName;
                    customerVM.LastName = customer.LastName;
                    customerVMList.Add(customerVM);
                }
            }
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);

            return View(customerVMList);
        }

        // GET: Customer/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            CustomerViewModel customerVM = new CustomerViewModel();
            Customer customer = GetCustomer(id);

            if (customer != null)
            {
                customerVM.CustomerId = customer.Id;
                customerVM.FirstName = customer.FirstName;
                customerVM.LastName = customer.LastName;
            }

            return View(customerVM);
        }

        public Customer GetCustomer(int id)
        {
            restProperties.Method = "EditCustomer";
            restProperties.MethodType = Method.GET;

            restProperties.Parameters = new List<CIRestParameter>();
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "id",
                value = id
            });

            IRestResponse<Customer> response = CIRestService.ExecuteRestRequest<Customer>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(response.Content);
            }
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CustomerViewModel customerVM)
        {
            try
            {
                Customer customer = new Customer();
                customer.Id = customerVM.CustomerId;
                customer.FirstName = customerVM.FirstName;
                customer.LastName = customerVM.LastName;

                CIRestProperties restProperties = new CIRestProperties();
                restProperties.Controller = typeof(Customer).Name;
                restProperties.Method = "SaveCustomer";
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
                    customer = JsonConvert.DeserializeObject<Customer>(response.Content);
                else
                    throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);


                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            CustomerViewModel customerVM = new CustomerViewModel();
            restProperties.Method = "GetCustomerwithContacts";
            restProperties.MethodType = Method.GET;

            restProperties.Parameters = new List<CIRestParameter>();
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "id",
                value = id
            });

            IRestResponse<Customer> response = CIRestService.ExecuteRestRequest<Customer>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(response.Content);
                if (customer != null)
                {
                    customerVM.CustomerId = customer.Id;
                    customerVM.FirstName = customer.FirstName;
                    customerVM.LastName = customer.LastName;
                    customerVM.CustomerContacts = new List<CustomerContactViewModel>();
                    foreach (CustomerContact contact in customer.CustomerContacts)
                    {
                        CustomerContactViewModel contactVM = new CustomerContactViewModel();
                        contactVM.CutsomerContactId = contact.Id;
                        contactVM.CustomerId = contact.CustomerId;
                        contactVM.ContactTypeId = contact.ContactTypeId;
                        contactVM.ContactTypeText = contact.ContactType?.Type;
                        contactVM.ContactValue = contact.ContactValue;
                        contactVM.ContactStatus = contact.ContactStatus;
                        customerVM.CustomerContacts.Add(contactVM);
                    }
                }
            }
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);

            return View(customerVM);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            CustomerViewModel customerVM = new Models.CustomerViewModel();
            customerVM.CustomerId = 0;
            return View(customerVM);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerViewModel customerVM)
        {
            try
            {
                Customer customer = new Customer();
                customer.Id = customerVM.CustomerId;
                customer.FirstName = customerVM.FirstName;
                customer.LastName = customerVM.LastName;

                CIRestProperties restProperties = new CIRestProperties();
                restProperties.Controller = typeof(Customer).Name;
                restProperties.Method = "SaveCustomer";
                restProperties.MethodType = Method.POST;
                restProperties.Parameters = new List<CIRestParameter>();
                restProperties.Parameters.Add(new CIRestParameter()
                {
                    key = "customer",
                    value = customer
                });

                IRestResponse<Customer> response = CIRestService.ExecuteRestRequest<Customer>(restProperties, client);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(response.Content);

                    if (customer.Id > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
                    }
                }
                else
                    throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            CustomerViewModel customerVM = new CustomerViewModel();
            Customer customer = GetCustomer(id);

            if (customer != null)
            {
                customerVM.CustomerId = customer.Id;
                customerVM.FirstName = customer.FirstName;
                customerVM.LastName = customer.LastName;
            }
            return View(customerVM);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                restProperties.Method = "DeleteCustomer";
                restProperties.MethodType = Method.GET;
                restProperties.Parameters = new List<CIRestParameter>();
                restProperties.Parameters.Add(new CIRestParameter()
                {
                    key = "id",
                    value = id
                });

                IRestResponse<bool> response = CIRestService.ExecuteRestRequest<bool>(restProperties, client);

                if (response.Data)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
                }
                // TODO: Add delete logic here
            }
            catch
            {
                return View();
            }
        }
    }
}
