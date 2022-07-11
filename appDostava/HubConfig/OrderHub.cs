using Contracts.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Web.Http;

namespace appDostava.HubConfig
{

    [Microsoft.AspNetCore.Authorization.Authorize(Roles = RolesConstants.ConsumerDeliverer)]
    public class OrderHub : Hub
    {
        public async Task PushTimerData(string username, int timer) =>
            await Clients.User(username).SendAsync("pushTimer", new { Timer = timer });
    }
}
