using Contracts.Dtos.Order.Post.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos.Order.Post
{
    public class PostOrderDto
    {

        [Required(AllowEmptyStrings = false,ErrorMessage = "Consumer's username is required")]
        public string Consumer { get; set; }

        [Required(ErrorMessage = "Empty orders aren't allowed")]
        [ProductListValidator(ErrorMessage = "Product list can't be empty")]

        public List<long> ProductIds { get; set; }

    }
}
