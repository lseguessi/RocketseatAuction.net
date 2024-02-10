using RocketseatAuction.API.Communication.Request;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Services.Offers.CreateOffer
{
    public class CreateOfferUseCase
    {

        private readonly LoggedUser _loggedUser;

        public CreateOfferUseCase()
        {
        }

        public CreateOfferUseCase(LoggedUser loggedUser) => _loggedUser = loggedUser;

        public int Execute(int ItemId, RequestCreateOfferJson request)
        {
            var repository = new RocketseatAuctionDbContext();

            var loggedUser = _loggedUser.User();

            var offer = new Offer
            {
                CreatedOn = DateTime.Now,
                Price = request.Price,
                ItemId = ItemId,
                UserId = loggedUser.Id
            };

            repository.Offers.Add(offer);

            repository.SaveChanges();

            return offer.Id;
        }
    }
}
