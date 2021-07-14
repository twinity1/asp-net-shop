using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DyShop.Data.Entities;
using DyShop.Helpers.Validation;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Models
{
    public class CartCheckoutViewModel : IValidatableObject
    {
        [BindProperty]
        public AddressViewModel BillingAddress { get; set; }

        [BindProperty]
        [ValidateIf(nameof(UseDeliveryAddress))]
        public AddressViewModel? DeliveryAddress { get; set; }
        
        [BindProperty]
        public bool UseDeliveryAddress { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Je potřeba vybrat jednu z možností dopravy.")]
        public int DeliveryId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Je potřeba vybrat jednu z možností plateb.")]
        public int PaymentId { get; set; }

        [BindProperty]
        [BooleanRequired(ErrorMessage = "Je potřeba souhlasit s obchodními podmínkami.")]
        public bool AcceptTermsAndConditions { get; set; }

        public float TotalPrice { get; set; }
        
        public Cart Cart { get; set; }

        public List<Delivery> Deliveries { get; set; } = new List<Delivery>();

        public List<Payment> Payments { get; set; } = new List<Payment>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            
            if (Deliveries.Any(x => x.Id == DeliveryId) == false)
            {
                result.Add(new ValidationResult("Neplatná doprava.", new []{nameof(DeliveryId)}));
            }
            if (Payments.Any(x => x.Id == PaymentId) == false)
            {
                result.Add(new ValidationResult("Neplatná platební metoda.", new []{nameof(PaymentId)}));
            }

            if (Cart.Items.Any() == false)
            {
                result.Add(new ValidationResult("V košíku nejsou žádné produkty.", new []{nameof(Cart)}));
            }

            return result;
        }

        [BindProperties]
        public class AddressViewModel
        {
            [Required(ErrorMessage = "Pole jméno je povinné.")]
            public string? Name { get; set; }
        
            [Phone(ErrorMessage = "Telefonní číslo je v chybném formátu (999 999 999).")]
            [Required(ErrorMessage = "Pole telefonní číslo je povinné.")]
            public string? Phone { get; set; }
        
            [Required(ErrorMessage = "Pole email je povinné.")]
            [EmailAddress(ErrorMessage = "Email je v chybném formátu.")]
            public string? Email { get; set; }
        
            [Required(ErrorMessage = "Pole město je povinné.")]
            public string? City { get; set; }
        
            [Required(ErrorMessage = "Pole ulice je povinné.")]
            public string? Street { get; set; }
        
            [Required(ErrorMessage = "Pole poštovní směrovací číslo je povinné.")]
            public string? ZipCode { get; set; }    
            
            public string? Note { get; set; }
        }
    }
}