using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Entities
{
    public class Staff : BaseEntity
    {
        public string EmploymentNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
