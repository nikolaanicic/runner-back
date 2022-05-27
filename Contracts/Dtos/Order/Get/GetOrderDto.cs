using Contracts.Dtos.Product.Get;
using System.Collections.Generic;

namespace Contracts.Dtos.Order.Get
{
    public class GetOrderDto
    {
        
        public long Id { get; set; }
        public string Consumer { get; set; }
        public string Deliverer { get; set; }
        public string OrderStatus { get; set; }
        public float TotalPrice { get; set; }
        public List<GetProductDto> Produce { get; set; }
    }
}
