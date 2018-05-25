using DCT.ILR.ValidationService.Models.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DCT.ILR.ValidationService.Models.Interfaces
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
