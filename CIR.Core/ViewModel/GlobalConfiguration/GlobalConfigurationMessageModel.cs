using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.ViewModel.GlobalConfiguration
{
    public class GlobalConfigurationMessageModel
    {
        public long Id { get; set; }

        public short Type { get; set; }

        public string Content { get; set; }

        public long CultureId { get; set; }
        public string CultureName { get; set; }
    }
}

