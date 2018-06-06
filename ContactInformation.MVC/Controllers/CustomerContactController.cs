using ContactInformation.MVC.Models;
using ContactInformation.ReadModel;
using ContactInformation.ReadModel.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContactInformation.MVC.Controllers
{
    public class CustomerContactController : Controller
    {
        RestClient client;
        CIRestProperties restProperties;

        public CustomerContactController()
        {
            client = new RestClient(ConfigurationManager.AppSettings["BusinessServiceUrl"].ToString());
            restProperties = new CIRestProperties();
            restProperties.Controller = typeof(CustomerContact).Name;
        }
        //// GET: CustomerContact
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: CustomerContact/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: CustomerContact/Create
        public ActionResult Create(int customerId)
        {
            CustomerContactViewModel contactVm = new CustomerContactViewModel();
            List<ContactType> contactTypes;
            Dictionary<string, string> ContactStatusList;
            GetContactTypesandStatus(out contactTypes, out ContactStatusList);

            contactVm.ContactStatusList = new SelectList(ContactStatusList, "Key", "Value");
            contactVm.ContactTypeList = new SelectList(contactTypes, "Id", "Type");

            CustomerController customerController = new CustomerController();
            Customer customer = customerController.GetCustomer(customerId);

            contactVm.CustomerName = customer.FirstName + " " + customer.LastName;

            return View(contactVm);
        }

        // POST: CustomerContact/Create
        [HttpPost]
        public ActionResult Create(CustomerContactViewModel contcatVm)
        {

            try
            {
                CustomerContact contact = new CustomerContact();
                contact.CustomerId = contcatVm.CustomerId;
                contact.ContactTypeId = contcatVm.ContactTypeId;
                contact.ContactStatus = contcatVm.ContactStatus;
                contact.ContactValue = contcatVm.ContactValue;

                restProperties.Method = "SaveContact";
                restProperties.MethodType = Method.POST;
                //restProperties.Body = customer;
                restProperties.Parameters = new List<CIRestParameter>();
                restProperties.Parameters.Add(new CIRestParameter()
                {
                    key = "contact",
                    value = contact
                });

                IRestResponse<CustomerContact> response = CIRestService.ExecuteRestRequest<CustomerContact>(restProperties, client);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    contact = JsonConvert.DeserializeObject<CustomerContact>(response.Content);
                    return RedirectToAction("Details", "Customer", new { id = contact.CustomerId });
                }
                else
                    throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerContact/Edit/5

        [HttpGet]
        [Route("api/CustomerContact/Edit")]
        public ActionResult Edit(int id)
        {
            CustomerContactViewModel customerContactVm = GetCustomerContctInfo(id);

            return View(customerContactVm);
        }

        // POST: CustomerContact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CustomerContactViewModel contactVM)
        {
            try
            {
                CustomerContact contact = new CustomerContact();
                contact.Id = contactVM.CutsomerContactId;
                contact.ContactTypeId = contactVM.ContactTypeId;
                contact.CustomerId = contactVM.CustomerId;
                contact.ContactValue = contactVM.ContactValue;
                contact.ContactStatus = contactVM.ContactStatus;
                // TODO: Add update logic here

                CIRestProperties restProperties = new CIRestProperties();
                restProperties.Controller = typeof(CustomerContact).Name;
                restProperties.Method = "SaveContact";
                restProperties.MethodType = Method.POST;
                //restProperties.Body = customer;
                restProperties.Parameters = new List<CIRestParameter>();
                restProperties.Parameters.Add(new CIRestParameter()
                {
                    key = "contact",
                    value = contact
                });

                IRestResponse<CustomerContact> response = CIRestService.ExecuteRestRequest<CustomerContact>(restProperties, client);

                if (response.StatusCode == HttpStatusCode.OK)
                    contact = JsonConvert.DeserializeObject<CustomerContact>(response.Content);
                else
                    throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);

                return RedirectToAction("Details", "Customer", new { id = contact.CustomerId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerContact/Delete/5
        public ActionResult Delete(int id)
        {
            CustomerContactViewModel customerContactVm = GetCustomerContctInfo(id);

            return View(customerContactVm);

        }

        // POST: CustomerContact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CustomerContactViewModel contactVM)
        {
            try
            {
                CIRestProperties restProperties = new CIRestProperties();
                restProperties.Controller = typeof(CustomerContact).Name;
                restProperties.Method = "RemoveContact";
                restProperties.MethodType = Method.GET;
                restProperties.Parameters = new List<CIRestParameter>();
                restProperties.Parameters.Add(new CIRestParameter()
                {
                    key = "id",
                    value = id
                });

                IRestResponse<bool> response = CIRestService.ExecuteRestRequest<bool>(restProperties, client);

                if (response.StatusCode == HttpStatusCode.OK)
                    if (response.Data)
                        return RedirectToAction("Details", "Customer", new { id = contactVM.CustomerId });
                    else
                        RedirectToAction("Delete", new { id = id });

                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
                // TODO: Add delete logic here
            }
            catch
            {
                return View();
            }
        }


        private CustomerContactViewModel GetCustomerContctInfo(int id)
        {
            CustomerContactViewModel customerContactVm = new CustomerContactViewModel();
            List<ContactType> contactTypes;
            Dictionary<string, string> ContactStatusList;
            GetContactTypesandStatus(out contactTypes, out ContactStatusList);

            customerContactVm = new CustomerContactViewModel();
            restProperties.Controller = typeof(CustomerContact).Name;
            restProperties.Method = "GetContactInformation";
            restProperties.MethodType = Method.GET;

            restProperties.Parameters = new List<CIRestParameter>();
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "id",
                value = id
            });

            IRestResponse<CustomerContact> response = CIRestService.ExecuteRestRequest<CustomerContact>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                CustomerContact customerContact = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerContact>(response.Content);
                if (customerContact != null)
                {
                    customerContactVm.CutsomerContactId = customerContact.Id;
                    customerContactVm.CustomerId = customerContact.CustomerId;
                    customerContactVm.CustomerName = customerContact.Customer?.FirstName + " " + customerContact.Customer?.LastName;
                    customerContactVm.ContactTypeList = new SelectList(contactTypes, "Id", "Type", customerContact.ContactTypeId);
                    customerContactVm.ContactTypeId = customerContact.ContactTypeId;
                    customerContactVm.ContactTypeText = customerContact.ContactType?.Type;
                    customerContactVm.ContactValue = customerContact.ContactValue;
                    customerContactVm.ContactStatus = customerContact.ContactStatus;
                    customerContactVm.ContactStatusList = new SelectList(ContactStatusList, "Key", "Value", customerContact.ContactStatus);
                }
            }
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
            return customerContactVm;
        }

        private void GetContactTypesandStatus(out List<ContactType> contactTypes, out Dictionary<string, string> ContactStatusList)
        {
            restProperties.Method = "GetContactTypes";
            restProperties.MethodType = Method.GET;
            restProperties.Controller = typeof(ContactType).Name;
            IRestResponse<ContactType> contactTypeResponse = CIRestService.ExecuteRestRequest<ContactType>(restProperties, client);
            contactTypes = JsonConvert.DeserializeObject<List<ContactType>>(contactTypeResponse.Content);
            ContactStatusList = new Dictionary<string, string>()
            {
                {Status.Active.ToString(), Status.Active.ToString() },
                {Status.Inactive.ToString(), Status.Inactive.ToString() },
            };
        }
    }
}
