﻿using DC.ILR.ValidationService.Models;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DC.ILR.ValidationService.Models
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
