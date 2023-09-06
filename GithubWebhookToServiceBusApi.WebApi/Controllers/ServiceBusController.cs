using GithubWebhookToServiceBusApi.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace GithubWebhookToServiceBusApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBusController : ControllerBase
    {
        private readonly IServiceBusService _serviceBusService;
        public ServiceBusController(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }
        [HttpPost]
        [Route("SentToServiceBus")]
        public async Task<IActionResult> SentToServiceBus()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string payload = await reader.ReadToEndAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(payload);
                string commitId = jsonData.after;
                try
                {
                    await _serviceBusService.SendToServiceBus(jsonData);
                    return Ok("Added To Service Bus");
                }
                catch (Exception ex)
                {
                    return Ok(ex.ToString());
                }
            }
        }
    }
}
