using DataProcess.Contract;
using DataProcess.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessorApi.Controllers
{
    namespace DataProcessorApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BackgroundProcessController : ControllerBase
        {
            private readonly ILogger<BackgroundProcessController> logger;
            private readonly IDataProcessorService dataProcessorService;

            public BackgroundProcessController(ILogger<BackgroundProcessController> logger, IDataProcessorService dataProcessorService)
            {
                this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
                this.dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
            }


            [HttpPost("start/{id}")]
            public bool start(Guid id)
            {
                return dataProcessorService.StartBackgroundProcess(id);
            }

            [HttpGet("currentStatus/{id}")]
            public DataJobStatus GetCurrentStatus(Guid id)
            {
                return dataProcessorService.GetBackgroundProcessStatus(id);
            }

            [HttpGet("result/{id}")]
            public List<string> GetBackgroundProcessResults(Guid dataJobId)
            {
                return dataProcessorService.GetBackgroundProcessResults(dataJobId);
            }
        }
    }
}
