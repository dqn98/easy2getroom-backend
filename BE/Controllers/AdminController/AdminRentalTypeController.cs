using BE.Application.Interfaces;
using BE.Data.Entities;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRentalTypeController : ControllerBase
    {
        private IRentalTypeService _rentalTypeService;
        private IUnitOfWork _unitOfWork;

        public AdminRentalTypeController(IRentalTypeService rentalTypeService, IUnitOfWork unitOfWork)
        {
            _rentalTypeService = rentalTypeService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllRentalTypes")]
        public async Task<List<RentalType>> GetAllRentalTypes()
        {
            return await _rentalTypeService.GetAll(); 
        }
    }
}