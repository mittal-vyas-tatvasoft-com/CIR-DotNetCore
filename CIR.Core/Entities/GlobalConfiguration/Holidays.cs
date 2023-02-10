using System.ComponentModel.DataAnnotations;

namespace CIR.Core.Entities.GlobalConfiguration
{
    public class Holidays
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long CountryId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
