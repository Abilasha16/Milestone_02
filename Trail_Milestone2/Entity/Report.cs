using System.ComponentModel.DataAnnotations;

namespace Trail_Milestone2.Entity
{
    public class Report
    {
        [Key]
        public Guid ReportId { get; set; }
        public string ReportType { get; set; }
    }
}
