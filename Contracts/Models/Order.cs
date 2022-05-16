using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{

    public enum OrderStatus { WAITING,DELIVERING,DELIVERED};

    public class Order
    {
        public long Id { get; set; }
        public Consumer Consumer { get; set; }
        public long ConsumerId { get; set; }

        public Deliverer Deliverer { get; set; }
        public long DelivererId { get; set; }

        public List<Product> Produce { get; set; }
        public float TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public float DeliveryTimer { get; set; }

    }
}
