namespace Trail_Milestone2.DTO.Response
{
    public class CustomerResponse
    {
        public Guid CustomerId { get; set; }
        public string FullName { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
