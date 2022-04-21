using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Entities
{
    public class Initiator : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Report> Reports { get; set; } = new List<Report>();
    }
}
