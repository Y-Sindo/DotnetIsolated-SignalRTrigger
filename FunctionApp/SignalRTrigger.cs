using Microsoft.Azure.Functions.Worker;

namespace signalrdotnetisolatedtrigger
{
    public class SignalRTrigger
    {

        [Function("sendMessage")]
        public string SendMessage(
            [SignalRTrigger("serverless", "messages", "sendMessage")] SignalRInvocationContext context)
        {
            return "This is message from SignalRTrigger";
        }
    }
}