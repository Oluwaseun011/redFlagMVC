using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string ReferenceNumber { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public IList<Staff> Staffs { get; set; }
    }
}
