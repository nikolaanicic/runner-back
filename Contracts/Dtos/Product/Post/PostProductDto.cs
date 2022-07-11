using System.ComponentModel.DataAnnotations;

namespace Contracts.Dtos.Product.Post
{
    public class PostProductDto
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "Products name is required")]
        [MaxLength(60,ErrorMessage = "Products must be 60 characters or shorter")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Products price is required")]
        public float Price { get; set; }
        
        [Required(AllowEmptyStrings = false,ErrorMessage = "Products details are required")]
        [MaxLength(400,ErrorMessage = "Products details must be 600 characters or shorter")]
        public string Details { get; set; }
    }
}
