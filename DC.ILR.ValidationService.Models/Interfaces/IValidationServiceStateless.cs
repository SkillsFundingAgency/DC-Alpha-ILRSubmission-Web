using DC.ILR.ValidationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DC.ILR.ValidationService.Models.Interfaces
{
    [ServiceContract]
    public interface IValidationServiceStateless
    {
        [OperationContract]
        Task<bool> Validate(IlrContext ilrContext);

        [OperationContract]
        Task<IEnumerable<string>> GetResults(Guid correlationId);
    }
}
