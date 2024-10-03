﻿using System.ComponentModel.DataAnnotations;
using Trail_Milestone2.Entity;

namespace Trail_Milestone2.DTO.Response
{
    public class RentalResponse
    {
        [Key]
        public Guid RentalId { get; set; }

        public Guid MotorbikeId { get; set; }
        public MotorBike MotorBike { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool OverdueStatus { get; set; }
        public string RentalStatus { get; set; }
    }
}
