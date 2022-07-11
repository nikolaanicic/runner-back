using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Contracts.HubConfig
{
    public class OrderHub : Hub
    {
        public async Task PushTimerData(string username, int timer) =>
            await Clients.User(username).SendAsync("pushTimer", new { Timer = timer });
    }
}
