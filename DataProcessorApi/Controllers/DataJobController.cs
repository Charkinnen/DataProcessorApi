using DataProcess.Contract;
using DataProcess.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataJobController : ControllerBase
    {
        private readonly ILogger<DataJobController> logger;
        private readonly IDataProcessorService dataProcessorService;

        public DataJobController(ILogger<DataJobController> logger, IDataProcessorService dataProcessorService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<DataJobDTO> Get()
        {
            return dataProcessorService.GetAllDataJobs();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public DataJobDTO Get(Guid id)
        {
            return dataProcessorService.GetDataJob(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] DataJobDTO dataJob)
        {
            dataProcessorService.Create(dataJob);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] DataJobDTO dataJob)
        {
            dataJob.Id = id;
            dataProcessorService.Update(dataJob);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            dataProcessorService.Delete(id);
        }
    }
}
