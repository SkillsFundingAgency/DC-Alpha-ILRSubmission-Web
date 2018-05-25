using Microsoft.ServiceFabric.Services.Remoting;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DCT.ILR.ValidationService.Models.Interfaces
{
    [ServiceContract]
    public interface IValidationServiceResults : IService
    {
        [OperationContract]
        Task SaveResultsAsync(string correlationId, IEnumerable<string> leanerValidationErrors);

        [OperationContract]
        Task<IEnumerable<string>> GetResultsAsync(string correlationId);
    }
}
