using System.Collections.Generic;

namespace Contracts.Models
{
    public enum ProfileState { DENIED,PROCESSING,APPROVED}

    public class Deliverer : User
    {
        public ProfileState State { get; set; }
        public List<Order> Orders { get; set; }
    }
}
