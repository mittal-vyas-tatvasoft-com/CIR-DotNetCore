using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.Entities.GlobalConfiguration
{
    public partial class GlobalConfigurationStyle
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        
        public string Description { get; set; } = null!;
        [Required]
        public int TypeCode { get; set; }
        [Required]
        public string TypeName { get; set; } = null!;
        [Required]
        public string ValueType { get; set; } = null!;
        [Required]
        public string Value { get; set; } = null!;
        [Required]
        public double SortOrder { get; set; }

    }
}
