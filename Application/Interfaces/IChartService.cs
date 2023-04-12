using BE.Application.ViewModels.Admin.Charts;
using System;

namespace BE.Application.Interfaces
{
    public interface IChartService : IDisposable
    {
        PropertyChartResultViewModel GetPropertyChart(string year);

        TotalPropertiesChartResultViewModel GetTotalPropertyChart(string year);

        PropertyCategoryChartResultViewModel GetPropertyCategoryChart(string year);

        RentalTypeChartResultViewModel GetRentalTypeChart(string year);
    }
}