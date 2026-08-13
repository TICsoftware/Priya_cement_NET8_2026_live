using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Priya_Cement_MVC.Models
{
    public class TechnicalSupportEnquiryModal
    {
        public int Id { get; set; }

        [Required]
        public int ServiceTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }


        public int CityId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public string City { get; set; }

        public int TestTypeId { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept the consent.")]
        public bool Consent { get; set; }






    }
}