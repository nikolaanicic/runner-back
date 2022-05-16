using System.Collections.Generic;

namespace Contracts.Models
{
    public class Consumer : User
    {
        public List<Order> Orders { get; set; }
    }
}
