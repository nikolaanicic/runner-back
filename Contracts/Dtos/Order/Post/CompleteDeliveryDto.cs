using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos.Order.Post
{
    public class CompleteDeliveryDto
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "Delivery id is needed")]
        public long DeliveryId { get; set; }
    }
}
