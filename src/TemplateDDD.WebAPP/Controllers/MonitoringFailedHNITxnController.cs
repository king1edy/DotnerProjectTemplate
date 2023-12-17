using Microsoft.AspNetCore.Mvc;
using TemplateDDD.APP.Common;
using TemplateDDD.Core.Services.Interfaces;
using TemplateDDD.Data.Dtos.MonitorFailedHNITxn;
using TemplateDDD.Shared.Constants;
using TemplateDDD.Shared.Responses;

namespace TemplateDDD.APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringFailedHNITxnController : RootController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MonitoringFailedHNITxnController> _logger;
        private readonly IMonitoringFailedHNITxnService _monitoringFailedHNITxnService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="monitoringFailedHNITxnService"></param>
        /// <param name="appsettings"></param>
        public MonitoringFailedHNITxnController(IConfiguration configuration, ILogger<MonitoringFailedHNITxnController> logger, IMonitoringFailedHNITxnService monitoringFailedHNITxnService)
        {
            _configuration = configuration;
            _logger = logger;
            _monitoringFailedHNITxnService = monitoringFailedHNITxnService;
        }


        /// <summary>
        /// Create MonitoringFailedTxnEx
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMonitoringFailedHNITxnDto model)
        {
            try
            {
                var result = await _monitoringFailedHNITxnService.CreateAsync(model, string.Empty, CurrentDateTime);

                if (!result.HasError)
                {
                    return Ok(ApiResponse<MonitoringFailedHNITxnDto>(result.Data));
                }

                return Ok(ApiResponse<string[]>(result.ErrorMessages.ToArray()));
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex.Message, ex);
                string[] errors = { ErrorConstants.ErrorMessage000 };

                return Ok(ApiResponse<string[]>(errors));
            }
        }

        /// <summary>
        /// Delete Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _monitoringFailedHNITxnService.DeleteAsync(id);

                if (!result.HasError)
                {
                    return Ok(ApiResponse<MonitoringFailedHNITxnDto>(result.Data));
                }

                return Ok(ApiResponse<string[]>(result.ErrorMessages.ToArray()));
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex.Message, ex);
                string[] errors = { ErrorConstants.ErrorMessage003 };
                return Ok(ApiResponse<string[]>(errors));
            }
        }

        /// <summary>
        /// Get Item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetItemAsync(Guid id)
        {
            try
            {
                var result = await _monitoringFailedHNITxnService.GetItemAsync(id);

                if (!result.HasError)
                {
                    return Ok(ApiResponse<MonitoringFailedHNITxnDto>(result.Data));
                }

                return Ok(ApiResponse<string[]>(result.ErrorMessages.ToArray()));
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex.Message, ex);

                string[] errors = { ErrorConstants.ErrorMessage003 };
                return Ok(ApiResponse<string[]>(errors));
            }
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _monitoringFailedHNITxnService.GetAllAsync();

                if (!result.HasError)
                {
                    return Ok(ApiResponse<List<MonitoringFailedHNITxnDto>>(result.Data));
                }

                return Ok(ApiResponse<string[]>(result.ErrorMessages.ToArray()));
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex.Message, ex);

                string[] errors = { ErrorConstants.ErrorMessage003 };
                return Ok(ApiResponse<string[]>(errors));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateMonitoringFailedHNITxnDto model)
        {
            try
            {
                var result = await _monitoringFailedHNITxnService.UpdateAsync(id, model, string.Empty, CurrentDateTime);

                if (!result.HasError)
                {
                    return Ok(ApiResponse<MonitoringFailedHNITxnDto>(result.Data));
                }

                return Ok(ApiResponse<string[]>(result.ErrorMessages.ToArray()));
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex.Message, ex);
                string[] errors = { ErrorConstants.ErrorMessage000 };
                return Ok(ApiResponse<string[]>(errors));
            }
        }
    }
}
