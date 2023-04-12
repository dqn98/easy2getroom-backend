using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Charts;
using BE.Data.Enums;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using BE.Ultilities.Constants;
using System;
using System.Linq;

namespace BE.Application.Implementations
{
    public class ChartService : IChartService
    {
        private IPropertyRepository _propertyRepository;
        private IUnitOfWork _unitOfWork;

        public ChartService(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PropertyCategoryChartResultViewModel GetPropertyCategoryChart(string year)
        {
            var result = new PropertyCategoryChartResultViewModel();

            var query = _propertyRepository.FindAll().Where(p => (year == null) ? true : p.DateCreated.Year.ToString().Equals(year));
            var total = query.Count();
            var totalRoom = query.Where(p => p.PropertyCategoryId == CommonConstants.RoomCategoryId).Count();
            var totalApartment = query.Where(p => p.PropertyCategoryId == CommonConstants.ApartmentCategoryId).Count();
            var totalHouse = query.Where(p => p.PropertyCategoryId == CommonConstants.HouseCategoryId).Count();

            result.Apartment = ((double)totalApartment / (double)total) * 100;
            result.House = ((double)totalHouse / (double)total) * 100;
            result.Room = 100 - (result.Apartment + result.House);
            return result;
        }

        public PropertyChartResultViewModel GetPropertyChart(string year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year.ToString();
            }
            var result = new PropertyChartResultViewModel();
            result.TotalProperties = new int[CommonConstants.TotalMonthsOfYear];
            result.TotalActiveProperties = new int[CommonConstants.TotalMonthsOfYear];
            result.TotalInactiveProperties = new int[CommonConstants.TotalMonthsOfYear];
            result.TotalAwaitingApprovalProperties = new int[CommonConstants.TotalMonthsOfYear];

            var query = _propertyRepository.FindAll();

            // The index variable start from 1
            for (int i = 0; i < CommonConstants.TotalMonthsOfYear; i++)
            {
                var totalPropertiesOfMonth = query.Where(x => x.DateCreated.Month.ToString().Equals((i + 1).ToString())).Count();
                var totalActivePropertiesOfMonth = query.Where(x => x.DateCreated.Month.ToString().Equals((i + 1).ToString()) && x.Status == Status.Active).Count();
                var totalInactivePropertiesOfMonth = query.Where(x => x.DateCreated.Month.ToString().Equals((i + 1).ToString()) && x.Status == Status.InActive).Count();
                var totalAwaitngApprovalPropertiesOfMonth = query.Where(x => x.DateCreated.Month.ToString().Equals((i + 1).ToString()) && x.Status == Status.AwaitingApproval).Count();

                result.TotalProperties[i] = totalPropertiesOfMonth;
                result.TotalActiveProperties[i] = totalActivePropertiesOfMonth;
                result.TotalInactiveProperties[i] = totalInactivePropertiesOfMonth;
                result.TotalAwaitingApprovalProperties[i] = totalAwaitngApprovalPropertiesOfMonth;
            }

            return result;
        }

        public RentalTypeChartResultViewModel GetRentalTypeChart(string year)
        {
            var result = new RentalTypeChartResultViewModel();

            var query = _propertyRepository.FindAll().Where(p => (year == null) ? true : p.DateCreated.Year.ToString().Equals(year));
            var total = query.Count();
            var forRent = query.Where(p => p.RentalTypeId == CommonConstants.ForRentId).Count();
            var needRent = query.Where(p => p.RentalTypeId == CommonConstants.NeedRentId).Count();
            var forSharing = query.Where(p => p.RentalTypeId == CommonConstants.ForSharingId).Count();
            var needSharing = query.Where(p => p.RentalTypeId == CommonConstants.NeedSharingId).Count();

            result.ForRent = ((double)forRent / (double)total) * 100;
            result.NeedRent = ((double)needRent / (double)total) * 100;
            result.ForSharing = ((double)forSharing / (double)total) * 100;
            result.NeedSharing = 100 - (result.ForRent + result.NeedRent + result.ForSharing);
            return result;
        }

        public TotalPropertiesChartResultViewModel GetTotalPropertyChart(string year)
        {
            var result = new TotalPropertiesChartResultViewModel();

            var query = _propertyRepository.FindAll().Where(p => (year == null) ? true : p.DateCreated.Year.ToString().Equals(year));
            var total = query.Count();
            var totalActive = query.Where(p => p.Status == Status.Active).Count();
            var totalInactive = query.Where(p => p.Status == Status.InActive).Count();
            var totalAwaitingApproval = query.Where(p => p.Status == Status.AwaitingApproval).Count();

            result.Active = ((double)totalActive / (double)total) * 100;
            result.InActive = ((double)totalInactive / (double)total) * 100;
            result.AwatingApproval = 100 - (result.Active + result.InActive);

            return result;
        }
    }
}