using Microsoft.AspNetCore.Http;
using redFlag.Entities;
using redFlag.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public int InitiatorId { get; set; }
        public string InitiatorName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public DisasterType DisasterType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string VoiceNote { get; set; }
        public string ShortVideo { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }

    public class CreateReportRequestModel
    {
        public DisasterType DisasterType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile VoiceNote { get; set; }
        public IFormFile ShortVideo { get; set; }
    }
}
