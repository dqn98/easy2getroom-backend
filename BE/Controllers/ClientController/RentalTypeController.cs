using BE.Application.Interfaces;
using BE.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalTypeController : ControllerBase
    {
        private IRentalTypeService _rentalTypeService;

        public RentalTypeController(IRentalTypeService rentalTypeService)
        {
            _rentalTypeService = rentalTypeService;
        }

        [HttpGet]
        [Route("GetRentalTypes")]
        public Task<List<RentalType>> GetRentalTypes()
        {
            return _rentalTypeService.GetAll();
        }
    }
}