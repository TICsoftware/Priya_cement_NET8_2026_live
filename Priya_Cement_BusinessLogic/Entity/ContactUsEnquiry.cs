namespace Priya_Cement_BusinessLogic.Entity
{
    public class ContactUsEnquiry
    {
        public int EnquiryId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Organisation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }
        public int StateId{ get; set; }
        public int InterestId { get; set; }
        public string Query { get; set; }
        public bool Consent { get; set; }
        public string IPAddress { get; set; }
    }
}
