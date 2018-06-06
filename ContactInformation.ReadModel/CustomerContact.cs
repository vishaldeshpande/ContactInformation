using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactInformation.ReadModel
{
    public class CustomerContact
    {
        public int Id { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual ContactType ContactType { get; set; }
        [ForeignKey("ContactType")]
        public int ContactTypeId { get; set; }
        public string ContactValue { get; set; }
        public string ContactStatus { get; set; }
    }
}
