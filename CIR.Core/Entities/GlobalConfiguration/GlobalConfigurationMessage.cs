using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.Entities.GlobalConfiguration
{
    public partial class GlobalConfigurationMessage
    {
        public long Id { get; set; }

        public short Type { get; set; }

        public string Content { get; set; } = null!;

        public long CultureId { get; set; }
    }
}
