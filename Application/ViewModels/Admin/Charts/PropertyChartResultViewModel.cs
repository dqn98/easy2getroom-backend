using System.Collections;

namespace BE.Application.ViewModels.Admin.Charts
{
    public class PropertyChartResultViewModel
    {
        public int[] TotalProperties { get; set; }
        public int[] TotalActiveProperties { get; set; }
        public int[] TotalInactiveProperties { get; set; }
        public int[] TotalAwaitingApprovalProperties { get; set; }
    }
}