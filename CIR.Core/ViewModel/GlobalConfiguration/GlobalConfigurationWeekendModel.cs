using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.ViewModel.GlobalConfiguration
{
    public class GlobalConfigurationWeekendModel
    { 
        public List<WeekendViewModel> WeekendList { get; set; }
        public int Count { get; set; }
    }

    public class WeekendViewModel
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long DayOfWeekId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string DayOfWeek { get; set; }
    }
}
