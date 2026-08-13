using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Priya_Cement_MVC.Models
{
    public class SolutionCenterEnquiryModal
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Phone]
        public string WhatsAppNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string AgeGroup { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public string TownVillage { get; set; }

        [Required]
        public string CurrentOccupation { get; set; }

        [Required]
        public string OwnShopOrCommercialSpace { get; set; }

        [Required]
        public string PreviouslyRunBusiness { get; set; }

        [Required]
        public string HaveSpaceForStoreSetup { get; set; }

        public decimal? StoreSetupSizeSqFt { get; set; }

        public string PreferredTimeForContact { get; set; }
    }
}