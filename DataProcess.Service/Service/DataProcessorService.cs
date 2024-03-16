using DataProcess.Contract;
using DataProcess.Interface;
using System.ComponentModel;

namespace DataProcess.Service.Service
{
    public class DataProcessorService : IDataProcessorService
    {
        private readonly IDataProcessRepository repository;

        public DataProcessorService(IDataProcessRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public DataJobDTO Create(DataJobDTO dataJob)
        {
            return repository.Create(dataJob);
        }

        public void Delete(Guid dataJobID)
        {
            repository.Delete(dataJobID);
        }

        public IEnumerable<DataJobDTO> GetAllDataJobs()
        {
            return repository.GetAll();
        }

        public List<string> GetBackgroundProcessResults(Guid dataJobId)
        {
            DataJobDTO dataJob = repository.GetById(dataJobId);
            if (dataJob == null)
            {
                throw new InvalidEnumArgumentException($"There are no process with id {dataJobId}");
            }
            if (dataJob.Status != DataJobStatus.Completed)
            {
                throw new InvalidOperationException($"Process {dataJobId} is not completed yet!");
            }
            return dataJob.Results.ToList();
        }

        public DataJobStatus GetBackgroundProcessStatus(Guid dataJobId)
        {
            DataJobDTO dataJob = repository.GetById(dataJobId);
            if(dataJob == null) 
            {
                throw new InvalidEnumArgumentException($"There are no process with id {dataJobId}");
            }
            return dataJob.Status;
        }

        public DataJobDTO GetDataJob(Guid dataJobId)
        {
            DataJobDTO dataJob = repository.GetById(dataJobId);
            if (dataJob == null)
            {
                throw new InvalidEnumArgumentException($"There are no process with id {dataJobId}");
            }
            return dataJob;
        }

        public IEnumerable<DataJobDTO> GetDataJobsByStatus(DataJobStatus status)
        {
            return repository.GetByStatus(status);
        }

        public bool StartBackgroundProcess(Guid dataJobId)
        {
            DataJobDTO dataJob = repository.GetById(dataJobId);
            if (dataJob == null)
            {
                throw new InvalidEnumArgumentException($"There are no process with id {dataJobId}");
            }
            if(dataJob.Status != DataJobStatus.New)
            {
                throw new InvalidOperationException($"Process {dataJobId} cannot be started, current status {dataJob.Status}");
            }
            dataJob.Status = DataJobStatus.Processing;
            repository.Update(dataJob);
            return true;
        }

        public DataJobDTO Update(DataJobDTO dataJob)
        {
            DataJobDTO currentDataJob = repository.GetById(dataJob.Id);
            if (currentDataJob == null)
            {
                throw new InvalidEnumArgumentException($"There are no process with id {dataJob.Id}");
            }

            currentDataJob.FilePathToProcess = dataJob.FilePathToProcess;
            currentDataJob.Links = dataJob.Links;
            currentDataJob.Results = dataJob.Results;

            repository.Update(currentDataJob);
            return currentDataJob;
        }
    }
}
