using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.Requests.Property;

namespace PMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IAddressService _addressService;



        public PropertyController(IPropertyService propertyService, IAddressService addressService)
        {
            _propertyService = propertyService;
            _addressService = addressService;
        }


        [HttpPost,Route("/Create")]
        [Authorize]
        public async Task<IActionResult> CreatePropertyAsync([FromBody] CreateProperty request)
        {
            var propertyId = Guid.NewGuid();
            var addressId = Guid.NewGuid();

            await _addressService.CreateAsync(addressId, request.Country, request.City, request.PostalCode, request.Street, request.BuildingNumber, 0);

            await _propertyService.CreateAsync(propertyId,Guid.Parse(User.Identity.Name),addressId,request.Name, request.Description, 0);


            return Created($"/Property/{propertyId}", null);
        }
    }
}