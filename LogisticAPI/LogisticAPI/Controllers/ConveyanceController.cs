using LogisticAPI.Models;
using LogisticAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConveyanceController : ControllerBase
    {
        private IConveyanceService @object;

        public ConveyanceController(IConveyanceService @object)
        {
            this.@object = @object;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlace(ConveyanceRequest request)
        {
            var result = await @object.CreateConveyance(request);
            return Ok(result);
        }
    }
}