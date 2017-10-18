using System.Collections.Generic;
using System.ComponentModel;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class PermissionModel
    {
        [DisplayName(@"Profile")]
        public int ProfileId { get; set; }

        public IEnumerable<Screen> ScreenList { get; set; }

        public IEnumerable<ProfileForScreen> ProfileForScreenList { get; set; }

        public IDictionary<int, string> ProfileDictionary { get; set; }

        public double Scroll { get; set; }
    }
}