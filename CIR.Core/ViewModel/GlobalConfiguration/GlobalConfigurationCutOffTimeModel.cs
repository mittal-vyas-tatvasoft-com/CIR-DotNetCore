using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.ViewModel.GlobalConfiguration
{
    public class GlobalConfigurationCutOffTimeModel
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public string CutOffTime { get; set; }
        public short CutOffDay { get; set; }
    }
}
