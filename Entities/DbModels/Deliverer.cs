using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DbModels
{

    public enum ProfileState { DENIED,PROCESSING,APPROVED}

    public class Deliverer : User
    {
        public ProfileState State { get; set; }
        public List<Order> Orders { get; set; }
    }
}
