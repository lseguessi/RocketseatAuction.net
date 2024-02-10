using Microsoft.AspNetCore.Mvc;
using RocketseatAuction.API.Communication.Request;
using RocketseatAuction.API.Filters;
using RocketseatAuction.API.Services.Offers.CreateOffer;

namespace RocketseatAuction.API.Controllers
{
    [ServiceFilter(typeof(AuthenticationUserAttribute))]
    public class OfferController : RocketseatAuctionBaseController

    {
        [HttpPost]
        [Route("{itemId:int}")]
        public IActionResult CreateOffer([FromRoute] int itemId, [FromBody]RequestCreateOfferJson request, [FromServices] CreateOfferUseCase useCase)
        {
            var id = useCase.Execute(itemId, request);

            return Created(string.Empty, id);
        }
    }
}
