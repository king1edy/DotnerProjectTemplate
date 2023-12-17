using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TemplateDDD.Core.Services.Interfaces;
using TemplateDDD.Data.Dtos.MonitorFailedHNITxn;
using TemplateDDD.Data.Entities;
using TemplateDDD.Shared.BaseService;
using TemplateDDD.Shared.Constants;
using TemplateDDD.Shared.Repository;
using TemplateDDD.Shared.Responses;
using TemplateDDD.Shared.UnitOfWork;

namespace TemplateDDD.Core.Services
{
    public class MonitoringFailedHNITxnService : BaseService<MonitoringFailedHNITxn, Guid>, IMonitoringFailedHNITxnService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MonitoringFailedHNITxnService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<MonitoringFailedHNITxnDto>> CreateAsync(CreateMonitoringFailedHNITxnDto model, string currentUserName, DateTime currentDateTime)
        {
            var response = new ServiceResponse<MonitoringFailedHNITxnDto>();

            if (model == null)
            {
                response.AddError(ErrorConstants.ErrorMessage000);
                return response;
            }

            if (string.IsNullOrEmpty(model.AccountName))
            {
                response.AddError(ErrorConstants.ErrorMessage031);

                return response;
            }

            if (response.HasError)
            {
                return response;
            }

            var data = _mapper.Map<CreateMonitoringFailedHNITxnDto, MonitoringFailedHNITxn>(model);
            await AddAsync(data);

            var result = _mapper.Map<MonitoringFailedHNITxn, MonitoringFailedHNITxnDto>(data);
            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse<MonitoringFailedHNITxnDto>> UpdateAsync(Guid id, UpdateMonitoringFailedHNITxnDto model, string currentUserName, DateTime currentDateTime)
        {
            var response = new ServiceResponse<MonitoringFailedHNITxnDto>();

            MonitoringFailedHNITxn? existingItem = await GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (model == null)
            {
                response.AddError(ErrorConstants.ErrorMessage000);

                return response;
            }

            if (existingItem == null)
            {
                response.AddError(ErrorConstants.ErrorMessage003);

                return response;
            }

            var update = _mapper.Map(model, existingItem);
            await UpdateAsync(id, update);

            var result = _mapper.Map<MonitoringFailedHNITxn, MonitoringFailedHNITxnDto>(existingItem);
            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse<List<MonitoringFailedHNITxnDto>>> GetAllAsync()
        {
            var response = new ServiceResponse<List<MonitoringFailedHNITxnDto>>();

            var tempList = await GetAll().ToListAsync();

            if (tempList == null)
            {
                response.AddError(ErrorConstants.ErrorMessage008);

                return response;
            }

            var entityQuery = tempList.Select(x => _mapper.Map<MonitoringFailedHNITxn, MonitoringFailedHNITxnDto>(x));

            var result = new List<MonitoringFailedHNITxnDto>(entityQuery);
            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse<MonitoringFailedHNITxnDto>> GetItemAsync(Guid id)
        {
            var response = new ServiceResponse<MonitoringFailedHNITxnDto>();

            MonitoringFailedHNITxn? existingItem = await GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (existingItem == null)
            {
                response.AddError(ErrorConstants.ErrorMessage008);

                return response;
            }
            var result = _mapper.Map<MonitoringFailedHNITxn, MonitoringFailedHNITxnDto>(existingItem);
            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse<MonitoringFailedHNITxnDto>> DeleteAsync(Guid id)
        {
            var response = new ServiceResponse<MonitoringFailedHNITxnDto>();

            MonitoringFailedHNITxn? existingItem = await GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (existingItem == null)
            {
                response.AddError(ErrorConstants.ErrorMessage008);

                return response;
            }

            var data = await DeleteAsync(existingItem);

            if (data == 0)
            {
                response.AddError(ErrorConstants.ErrorMessage003);
                return response;
            }

            var result = _mapper.Map<MonitoringFailedHNITxn, MonitoringFailedHNITxnDto>(existingItem);
            response.Data = result;

            return response;
        }
    }
}