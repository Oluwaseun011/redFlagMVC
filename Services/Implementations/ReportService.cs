using Microsoft.AspNetCore.Hosting;
using redFlag.Dtos;
using redFlag.Entities;
using redFlag.Repositories.Interfaces;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Device.Gpio;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace redFlag.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IInitiatorRepository _initiatorRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IWebHostEnvironment _webroot;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportService(IReportRepository reportRepository, IWebHostEnvironment webroot, IInitiatorRepository initiatorRepository, IAgencyRepository agencyRepository, IHttpContextAccessor httpContextAccessor)
        {
            _reportRepository = reportRepository;
            _initiatorRepository = initiatorRepository;
            _agencyRepository = agencyRepository;
            _webroot = webroot;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<BaseResponse<ReportDto>> Create(CreateReportRequestModel model)
        {

             

            var image = "";
            if (model.Image != null)
            {
                string imagePath = Path.Combine(_webroot.WebRootPath, "Image");
                Directory.CreateDirectory(imagePath);
                string userImageType = model.Image.ContentType.Split('/')[1];
                image = $"{Guid.NewGuid()}.{userImageType}";
                var rightpath = Path.Combine(imagePath, image);
                using (var filestream = new FileStream(rightpath, FileMode.Create))
                {
                    model.Image.CopyTo(filestream);
                }
            }

            var voiceNote = "";
            if (model.VoiceNote != null)
            {
                string voicePath = Path.Combine(_webroot.WebRootPath, "VoiceNote");
                Directory.CreateDirectory(voicePath);
                string voiceNoteType = model.VoiceNote.ContentType.Split('/')[1];
                voiceNote = $"{Guid.NewGuid()}.{voiceNoteType}";
                var rightpath = Path.Combine(voicePath, voiceNote);
                using (var filestream = new FileStream(rightpath, FileMode.Create))
                {
                    model.VoiceNote.CopyTo(filestream);
                }
            }

            var shortVideo = "";

            if (model.ShortVideo != null)
            {
                string videoPath = Path.Combine(_webroot.WebRootPath, "shortvid");
                Directory.CreateDirectory(videoPath);
                string videoType = model.ShortVideo.ContentType.Split('/')[1];
                shortVideo = $"{Guid.NewGuid()}.{videoType}";
                var rightpath = Path.Combine(videoPath, shortVideo);
                using (var filestream = new FileStream(rightpath, FileMode.Create))
                {
                    model.ShortVideo.CopyTo(filestream);
                }
            }
            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var initiator = await _initiatorRepository.Get(a => a.UserId == int.Parse(user));

            var report = new Report
            {

                InitiatorId = initiator.Id,
                ReferenceNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper(),
                DisasterType = model.DisasterType,
                Description = model.Description,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                ReportStatus = Enums.ReportStatus.initiated,
                Image = image,
                VoiceNote = voiceNote,
                ShortVideo = shortVideo,
                
            };

            await _reportRepository.Create(report);

            return new BaseResponse<ReportDto>
            {
                Message = "Report sent",
                Status = true,
                Data = new ReportDto
                {
                    Id = report.Id,
                    DisasterType = report.DisasterType,
                    Description = report.Description,
                }
            };
        }

        public async Task<BaseResponse<ReportDto>> Get(int id)
        {
            var report = await _reportRepository.Get(id);
            if (report == null) return new BaseResponse<ReportDto>
            {
                Message = "Report not found",
                Status = false,

            };
            return new BaseResponse<ReportDto>
            {
                Message = "Success",
                Status = true,
                Data = new ReportDto
                {
                    Id = report.Id,
                    InitiatorId = report.InitiatorId,
                    Description = report.Description,
                    VoiceNote = report.VoiceNote,
                    ShortVideo = report.ShortVideo,
                    Latitude = report.Latitude,
                    Longitude = report.Longitude,
                    ReportStatus = report.ReportStatus,
                    DisasterType = report.DisasterType,
                }
            };

        }

        public async Task<BaseResponse<ReportDto>> CancelReport(int id)
        {
            var report = await _reportRepository.Get(id);
            if (report == null) return new BaseResponse<ReportDto>
            {
                Message = "Report not found",
                Status = false,

            };

            report.ReportStatus = Enums.ReportStatus.Cancelled;
            _reportRepository.Save();

            return new BaseResponse<ReportDto>
            {
                Message = "Success",
                Status = true,
                Data = new ReportDto
                {
                    Id = report.Id,
                    Description = report.Description,
                    ReportStatus = report.ReportStatus,
                    DisasterType = report.DisasterType,
                }
            };

        }

        public async Task<BaseResponse<ReportDto>> Enroute(int id)
        {
            var report = await _reportRepository.Get(id);
            if (report == null) return new BaseResponse<ReportDto>
            {
                Message = "Report not found",
                Status = false,

            };

            report.ReportStatus = Enums.ReportStatus.Enroute;
            _reportRepository.Save();

            return new BaseResponse<ReportDto>
            {
                Message = "Success",
                Status = true,
                Data = new ReportDto
                {
                    Id = report.Id,
                    Description = report.Description,
                    ReportStatus = report.ReportStatus,
                    DisasterType = report.DisasterType,
                }
            };

        }

        public async Task<BaseResponse<ReportDto>> Arrived(int id)
        {
            var report = await _reportRepository.Get(id);
            if (report == null) return new BaseResponse<ReportDto>
            {
                Message = "Report not found",
                Status = false,

            };

            report.ReportStatus = Enums.ReportStatus.Arrived;
            _reportRepository.Save();

            return new BaseResponse<ReportDto>
            {
                Message = "Success",
                Status = true,
                Data = new ReportDto
                {
                    Id = report.Id,
                    Description = report.Description,
                    ReportStatus = report.ReportStatus,
                    DisasterType = report.DisasterType,
                }
            };

        }

        public async Task<BaseResponse<ReportDto>> Done(int id)
        {
            var report = await _reportRepository.Get(id);
            if (report == null) return new BaseResponse<ReportDto>
            {
                Message = "Report not found",
                Status = false,

            };

            report.ReportStatus = Enums.ReportStatus.Solved;
            _reportRepository.Save();

            return new BaseResponse<ReportDto>
            {
                Message = "Success",
                Status = true,
                Data = new ReportDto
                {
                    Id = report.Id,
                    Description = report.Description,
                    ReportStatus = report.ReportStatus,
                    DisasterType = report.DisasterType,
                }
            };

        }

        public async Task<BaseResponse<IEnumerable<ReportDto>>> GetAll()
        {
            var reports = await _reportRepository.GetAll();
            var listOfReports = reports.ToList().Select(report => new ReportDto
            {
                Id = report.Id,
                InitiatorId = report.InitiatorId,
                InitiatorName = report.Initiator.User.FirstName,
                Description = report.Description,
                VoiceNote = report.VoiceNote,
                ShortVideo = report.ShortVideo,
                Latitude = report.Latitude,
                Longitude = report.Longitude,
                ReportStatus = report.ReportStatus,
                DisasterType = report.DisasterType,
            });

            return new BaseResponse<IEnumerable<ReportDto>>
            {
                Message = "Success",
                Status = true,
                Data = listOfReports,
            };

        }

        public async Task<BaseResponse<IEnumerable<ReportDto>>> GetSelected(int id)
        {

            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var initiator = await _initiatorRepository.Get(a => a.UserId == int.Parse(user));

            var reports = await _reportRepository.GetSelected(a => a.InitiatorId == initiator.Id);

            return new BaseResponse<IEnumerable<ReportDto>>
            {
                Data = reports.Select(report => new ReportDto
                {
                    Id = report.Id,
                    InitiatorId = report.InitiatorId,
                    InitiatorName = report.Initiator.User.FirstName,
                    Description = report.Description,
                    VoiceNote = report.VoiceNote,
                    ShortVideo = report.ShortVideo,
                    Latitude = report.Latitude,
                    Longitude = report.Longitude,
                    ReportStatus = report.ReportStatus,
                    DisasterType = report.DisasterType,
                }),
                Message = "Successful",
                Status = true,

            };

        }

        public async Task<BaseResponse<IEnumerable<ReportDto>>> GetReportsByAgency(int agencyId)
        {

            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /*var agency = await _agencyRepository.Get(a => a.Staffs.)*/

            var initiator = await _initiatorRepository.Get(a => a.UserId == int.Parse(user));

            var reports = await _reportRepository.GetSelected(a => a.InitiatorId == initiator.Id);

            return new BaseResponse<IEnumerable<ReportDto>>
            {
                Data = reports.Select(report => new ReportDto
                {
                    Id = report.Id,
                    InitiatorId = report.InitiatorId,
                    InitiatorName = report.Initiator.User.FirstName,
                    Description = report.Description,
                    VoiceNote = report.VoiceNote,
                    ShortVideo = report.ShortVideo,
                    Latitude = report.Latitude,
                    Longitude = report.Longitude,
                    ReportStatus = report.ReportStatus,
                    DisasterType = report.DisasterType,
                }),
                Message = "Successful",
                Status = true,

            };

        }
    }
}
