using System.Collections.Generic;

namespace Entities.DbModels
{
    public class Consumer : User
    {
        public List<Order> Orders { get; set; }
    }
}
