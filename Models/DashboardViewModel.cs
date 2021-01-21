using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class DashboardViewModel
    {
        public int orderCount { get; set; }
/*        public int orderCountByMonth { get; set; }
*/        public int orderCountByDay { get; set; }
        public decimal totalRevenue { get; set; }
        public int orderInProcess { get; set; }
        public int orderInComplete { get; set; }
        public int orderInCancel { get; set; }

    }
}
