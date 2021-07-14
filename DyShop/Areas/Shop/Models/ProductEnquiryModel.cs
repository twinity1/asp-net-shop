using System.ComponentModel.DataAnnotations;

namespace DyShop.Areas.Shop.Models
{
    public class ProductEnquiryModel
    {
        [Url]
        public string Url { get; set; }
        
        [Required(ErrorMessage = "Toto pole je povinné.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Toto pole je povinné.")]
        [EmailAddress(ErrorMessage = "Email je ve špatném formátu.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Toto pole je povinné.")]
        [MaxLength(1500, ErrorMessage = "Zpráva je příliš dlouhá.")]
        public string Question { get; set; }
    }
}