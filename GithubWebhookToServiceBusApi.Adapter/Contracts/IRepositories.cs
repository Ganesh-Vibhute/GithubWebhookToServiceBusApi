using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubWebhookToServiceBusApi.Adapter.Contracts
{
    public interface IRepositories
    {
        public Task<string> SendToTopic(dynamic topic);
    }
}
