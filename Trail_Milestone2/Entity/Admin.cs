using System.ComponentModel.DataAnnotations;

namespace Trail_Milestone2.Entity
{
    public class Admin
    {
        [Key]
        public Guid AdminId { get; set; }
        public string Name { get; set; }
    }
}
