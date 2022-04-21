using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Interfaces
{
    public interface IReportService
    {
        Task<BaseResponse<ReportDto>> Create(CreateReportRequestModel model);
        Task<BaseResponse<ReportDto>> Get(int id);
        Task<BaseResponse<IEnumerable<ReportDto>>> GetAll();
        Task<BaseResponse<IEnumerable<ReportDto>>> GetSelected(int id);
        Task<BaseResponse<IEnumerable<ReportDto>>> GetReportsByAgency(int agencyId);
        Task<BaseResponse<ReportDto>> CancelReport(int id);
        Task<BaseResponse<ReportDto>> Enroute(int id);
        Task<BaseResponse<ReportDto>> Arrived(int id);
        Task<BaseResponse<ReportDto>> Done(int id);
    }
}
