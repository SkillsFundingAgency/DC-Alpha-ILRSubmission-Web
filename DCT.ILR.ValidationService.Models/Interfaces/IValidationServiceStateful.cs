using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using DCT.ILR.ValidationService.Models.Models;
using Microsoft.ServiceFabric.Services.Remoting;

namespace DCT.ILR.ValidationService.Models.Interfaces
{
    [ServiceContract]
    public interface IValidationServiceStateful : IService
    {
        [OperationContract]
        Task<bool> Validate(IlrContext ilrContext);

        [OperationContract]
        Task<List<string>> GetResults(Guid correlationId);
    }
}
