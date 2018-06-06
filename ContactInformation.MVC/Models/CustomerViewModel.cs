using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactInformation.MVC.Models
{
    public class CustomerViewModel
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public List<CustomerContactViewModel> CustomerContacts { get; set; }
    }
}