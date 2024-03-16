using DataProcess.Contract;
using DataProcess.Interface;

namespace DataProcess.Service.Repository
{
    public class InMemoryRepository : IDataProcessRepository
    {
        readonly Dictionary<Guid, DataJobDTO> _jobs;

        public InMemoryRepository()
        {
            _jobs = new Dictionary<Guid, DataJobDTO>();
        }

        public DataJobDTO Create(DataJobDTO job)
        {
            if(job.Id == Guid.Empty) 
            {
                job.Id = Guid.NewGuid();
            }
            _jobs.Add(job.Id, job);
            return job;
        }

        public void Delete(Guid dataJobID)
        {
            _jobs.Remove(dataJobID);
        }

        public IEnumerable<DataJobDTO> GetAll()
        {
            return _jobs.Values;
        }

        public DataJobDTO GetById(Guid dataJobId)
        {
            return _jobs[dataJobId];
        }

        public IEnumerable<DataJobDTO> GetByStatus(DataJobStatus status)
        {
            return _jobs.Values.Where(x => x.Status == status);
        }

        public void Update(DataJobDTO dataJob)
        {
            _jobs[dataJob.Id] = dataJob;
        }
    }
}
