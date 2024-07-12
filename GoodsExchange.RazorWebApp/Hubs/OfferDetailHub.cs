using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GoodsExchange.RazorWebApp.Hubs
{
    public class OfferDetailHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task OfferUpdated()
        {
            await Clients.All.SendAsync("OfferDetailUpdated");
        }
    }
}
