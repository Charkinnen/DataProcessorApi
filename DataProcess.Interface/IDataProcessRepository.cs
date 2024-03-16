using DataProcess.Contract;

namespace DataProcess.Interface
{
    public interface IDataProcessRepository
    {
        DataJobDTO Create(DataJobDTO job);
        void Delete(Guid dataJobID);
        IEnumerable<DataJobDTO> GetAll();
        DataJobDTO GetById(Guid dataJobId);
        IEnumerable<DataJobDTO> GetByStatus(DataJobStatus status);
        void Update(DataJobDTO dataJob);
    }
}
