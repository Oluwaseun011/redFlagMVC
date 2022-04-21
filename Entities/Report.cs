using redFlag.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Entities
{
    public class Report : BaseEntity
    {
        public string ReferenceNumber { get; set; }
        public int InitiatorId { get; set; }
        public Initiator Initiator { get; set; }
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
        public DisasterType DisasterType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string VoiceNote { get; set; }
        public string ShortVideo { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
