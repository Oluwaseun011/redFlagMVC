using redFlag.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Entities
{
    public class Agency : BaseEntity
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Abbreviation { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public DisasterType DisasterType { get; set; }
        public IList<Branch> Branches { get; set; }
        public IList<Staff> Staffs { get; set; }

    }
}
