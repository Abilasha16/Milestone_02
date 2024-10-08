﻿using System.ComponentModel.DataAnnotations;

namespace Trail_Milestone2.Entity
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string FullName { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public string LicenseNumber {  get; set; }
        public string PhoneNumber { get; set; }
    }
}
