using System.Threading.Tasks;

namespace DC.SF.IlrSubmission.Web.ServiceBus
{
    public interface IServiceBusQueue
    {
        Task SendMessagesAsync(string messageToSend, string sessionId);
    }
}