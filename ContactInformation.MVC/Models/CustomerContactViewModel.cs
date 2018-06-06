using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactInformation.MVC.Models
{
    public class CustomerContactViewModel
    {
        [Required]
        public int CutsomerContactId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public int ContactTypeId { get; set; }
        public SelectList ContactTypeList { get; set; }
        public string ContactTypeText { get; set; }
        [Required]
        public string ContactValue { get; set; }
        [Required]
        public string ContactStatus { get; set; }
        public SelectList ContactStatusList { get; set; }
    }
}