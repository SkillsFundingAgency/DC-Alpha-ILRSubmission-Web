using System;
using System.Diagnostics;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DC.SF.IlrSubmission.Web.Models;
using DC.SF.IlrSubmission.Web.ServiceBus;
using DCT.ILR.ValidationService.Models.Interfaces;
using DCT.ILR.ValidationService.Models.JsonSerialization;
using DCT.ILR.ValidationService.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace DC.SF.IlrSubmission.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValidationServiceResults _validationResultsService;

        private readonly Random _rnd = new Random();

        private readonly IServiceBusQueue _serviceBusQueueHelper;

        private readonly string StorageAccountName;

        private readonly string StorageAccountKey;

        public HomeController(IConfiguration configuration, IServiceBusQueue serviceBusQueueHelper, ConfigurationPackage configurationPackage)
        {
            IValidationServiceStateful _validationService = ServiceProxy.Create<IValidationServiceStateful>(
               new Uri("fabric:/DCT.ILR.Processing.POC/DCT.ILR.VadationServiceStateful"),
               new ServicePartitionKey(0));

            ConfigurationPackage _configurationPackage = configurationPackage;
            ServiceProxyFactory serviceProxyFactory = new ServiceProxyFactory(
               (c) => new FabricTransportServiceRemotingClientFactory(
                   serializationProvider: new ServiceRemotingJsonSerializationProvider()));

            _validationResultsService = serviceProxyFactory.CreateServiceProxy<IValidationServiceResults>(
               new Uri("fabric:/DCT.ILR.Processing.POC/DCT.ILR.Data"),
               new ServicePartitionKey(0L), TargetReplicaSelector.PrimaryReplica, "dataServiceRemotingListener");

            _serviceBusQueueHelper = serviceBusQueueHelper;
            StorageAccountName = _configurationPackage.Settings.Sections["StorageAccount"].Parameters["IlrFileStorageAccontName"].Value;
            StorageAccountKey = _configurationPackage.Settings.Sections["StorageAccount"].Parameters["IlrFileStorageAccontKey"].Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(500_000_000)]
        public async Task<IActionResult> SubmitILR(IFormFile file, bool IsShredAndProcess)
        {
            ViewData["Message"] = "Your application description page.";

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse($"DefaultEndpointsProtocol=https;AccountName={StorageAccountName};AccountKey={StorageAccountKey};EndpointSuffix=core.windows.net");
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("ilr-files");

            var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{GetRandomCharacters(5)}.xml";
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(newFileName);

            using (var outputStream = await cloudBlockBlob.OpenWriteAsync())
            {
                await file.CopyToAsync(outputStream);
            }

            //write it into a queue
            var correlationId = Guid.NewGuid();
            var model = new IlrContext()
            {
                CorrelationId = correlationId,
                ContainerReference = "ilr-files",
                Filename = newFileName,
                //Filename = $"ILR-10006341-1718-20171107-113456-04.xml",
                IsShredAndProcess = IsShredAndProcess
            };

            await _serviceBusQueueHelper.SendMessagesAsync(JsonConvert.SerializeObject(model), GetRandomCharacters(8));

            return RedirectToAction("Confirmation", new { correlationId });
        }

        public IActionResult Confirmation(Guid correlationId)
        {
            ViewData["CorrelationId"] = correlationId;

            return View();
        }

        public async Task<IActionResult> Status(Guid correlationId)
        {
            var resultsModel = await _validationResultsService.GetResultsAsync(correlationId.ToString());

            return View(resultsModel?.ToList());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetRandomCharacters(int length)
        {
            string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            var result = new string(
                Enumerable.Repeat(Characters, length)
                          .Select(s => s[_rnd.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}
