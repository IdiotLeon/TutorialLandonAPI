using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TutorialLandonAPI.Models;

namespace TutorialLandonAPI.Controllers
{
    [Route("/[controller]")]
    public class InfoController:Controller
    {
        private readonly HotelInfo _hotelInfo;

        public InfoController(IOptions<HotelInfo> hotelInfoAccessor)
        {
            _hotelInfo = hotelInfoAccessor.Value;
        }

        [HttpGet(Name = nameof(GetInfo))]
        public IActionResult GetInfo()
        {
            _hotelInfo.Href = Url.Link(nameof(GetInfo), null);

            return Ok(_hotelInfo);
        }
    }
}
