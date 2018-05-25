using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace DCT.ILR.ValidationService.Models.Interfaces
{
    [ServiceContract]
    public interface IReferenceDataService : IService
    {
        [OperationContract]
        Task InsertLARSData();

        [OperationContract]
        Task<IDictionary<string, string>> GetLARSLearningDeliveriesAsync(IEnumerable<string> learnAimRefs);

        [OperationContract]
        Task<string> GetLARSFromDB(IEnumerable<string> learnAimRefs);

        [OperationContract]
        Task InsertULNs();

        [OperationContract]
        Task<IEnumerable<long>> GetULNs(IEnumerable<long> ulns);
    }
}
