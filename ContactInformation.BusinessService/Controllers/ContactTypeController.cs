using ContactInformation.ReadModel;
using ContactInformation.ReadModel.Shared;
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
    public class ContactTypeController : ApiController
    {
        RestClient client;
        CIRestProperties restProperties;

        public ContactTypeController()
        {
            client = new RestClient(ConfigurationManager.AppSettings["DataServiceUrl"].ToString());
            restProperties = new CIRestProperties();
            restProperties.Controller = typeof(ContactType).Name;
            restProperties.Parameters = new List<CIRestParameter>();
        }

        [HttpGet]
        [Route("api/ContactType/GetContactTypes")]
        public List<ContactType> GetContactTypes()
        {
            restProperties.Method = "GetContactTypes";
            restProperties.MethodType = Method.GET;

            IRestResponse<List<ContactType>> response = CIRestService.ExecuteRestRequest<List<ContactType>>(restProperties, client);

            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;
            else
                throw new Exception("Error Message " + response.ErrorMessage + "\n  Exception:" + response.ErrorException);
        }
    }
}
