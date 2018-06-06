using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactInformation.ReadModel
{
    public class ContactType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
    }
}
