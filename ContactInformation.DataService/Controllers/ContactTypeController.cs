using ContactInformation.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactInformation.DataService.Controllers
{
    public class ContactTypeController : ApiController
    {
        IGenericRepository<ContactType> contactTypeRepository;
        public ContactTypeController(IGenericRepository<ContactType> repository)
        {
            this.contactTypeRepository = repository;
        }

        [HttpGet]
        [Route("api/ContactType/GetContactTypes")]
        public List<ContactType> GetContactTypes()
        {
            return contactTypeRepository.GetAll();
        }
    }
}
