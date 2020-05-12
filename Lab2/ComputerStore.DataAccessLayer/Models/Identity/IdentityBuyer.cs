using Microsoft.AspNetCore.Identity;

namespace ComputerStore.DataAccessLayer.Models.Identity
{
    public class IdentityBuyer : IdentityUser
    {
        public int BuyerId { get; set; }
    }
}