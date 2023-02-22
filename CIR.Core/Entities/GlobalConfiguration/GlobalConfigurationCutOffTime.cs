using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.Entities.GlobalConfiguration
{
    public class GlobalConfigurationCutOffTime
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long CountryId { get; set; }
        [Required]
        public TimeSpan CutOffTime { get; set; }
        [Required]
        public short CutOffDay { get; set; }

    }
}
