using LogisticAPI.models;
using LogisticAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : ControllerBase
    {
        private IPlaceService @object;

        public PlaceController(IPlaceService @object)
        {
            this.@object = @object;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlace(PlaceRequest request)
        {
            var result = await @object.CreatePlace(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaces()
        {
            var result = await @object.GetPlaces();
            return Ok(result);
        }
    }
}