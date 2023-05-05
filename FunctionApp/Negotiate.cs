using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace signalrdotnetisolatedtrigger
{
    public class Negotiate
    {
        [Function("negotiate")]
        public string Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, [SignalRConnectionInfoInput(HubName = "serverless")] string SignalRconnectionInfo)
        {
            return SignalRconnectionInfo;
        }
    }
}
