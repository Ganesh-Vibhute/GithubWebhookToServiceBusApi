using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GithubWebhookToServiceBusApi.BLL.Contracts
{
    public interface IServiceBusService
    {
        public  Task<string> SendToServiceBus(dynamic serviceBus);
    }
}
