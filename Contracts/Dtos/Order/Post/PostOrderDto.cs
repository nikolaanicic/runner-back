using Contracts.Dtos.Order.Post.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos.Order.Post
{
    public class PostOrderDto
    {

        [Required(ErrorMessage = "Empty orders aren't allowed")]
        [ProductListValidator(ErrorMessage = "Product list can't be empty")]

        public List<long> ProductIds { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(200,ErrorMessage = "Comment can't be longer than 200 characters")]

        public string Comment { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Delivery address is required")]
        public string Address { get; set; }
    }
}
