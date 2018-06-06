using ContactInformation.ReadModel;
using ContactInformation.ReadModel.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactInformation.BusinessService.Controllers
{
    public class CustomerContactController : ApiController
    {
        RestClient client;
        CIRestProperties restProperties;
        public CustomerContactController()
        {
            client = new RestClient(ConfigurationManager.AppSettings["DataServiceUrl"].ToString());
            restProperties = new CIRestProperties();
            restProperties.Controller = typeof(CustomerContact).Name;
            restProperties.Parameters = new List<CIRestParameter>();
        }

        [HttpGet]
        [Route("api/CustomerContact/GetCustomerContact")]
        public CustomerContact GetCustomerContact(int id)
        {
            restProperties.Method = "Edit";
            restProperties.MethodType = Method.GET;

            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "id",
                value = id
            });

            IRestResponse<CustomerContact> response = CIRestService.ExecuteRestRequest<CustomerContact>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }

        [HttpGet]
        [Route("api/CustomerContact/GetContactInformation")]
        public CustomerContact GetContactInformation(int id)
        {
            restProperties.Method = "GetContactInformation";
            restProperties.MethodType = Method.GET;
            
            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "id",
                value = id
            });

            IRestResponse<CustomerContact> response = CIRestService.ExecuteRestRequest<CustomerContact>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<CustomerContact>(response.Content);
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }

        [HttpPost]
        [Route("api/CustomerContact/SaveContact")]
        public CustomerContact SaveContact(CustomerContact contact)
        {
            restProperties.Method = "Save";
            restProperties.MethodType = Method.POST;

            restProperties.Parameters.Add(new CIRestParameter()
            {
                key = "customerContact",
                value = contact
            });

            IRestResponse<CustomerContact> response = CIRestService.ExecuteRestRequest<CustomerContact>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }

        [HttpGet]
        [Route("api/CustomerContact/RemoveContact")]
        public bool RemoveContact(int id) 
        {
            restProperties.Method = "Delete";
            restProperties.MethodType = Method.GET;

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
