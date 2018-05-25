﻿using Microsoft.ServiceFabric.Services.Remoting;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DCT.ILR.ValidationService.Models.Interfaces
{ 

    [ServiceContract]
    public interface IULNv2DataService : IService
    {
        [OperationContract]
        Task InsertULNs(IEnumerable<long> ulns);

        [OperationContract]
        Task<IEnumerable<long>> GetULNs(IEnumerable<long> ulns);

        [OperationContract]
        Task<bool> ClearULNs();
    }
}
