
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DC.ILR.ValidationService.Models.Interfaces
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
