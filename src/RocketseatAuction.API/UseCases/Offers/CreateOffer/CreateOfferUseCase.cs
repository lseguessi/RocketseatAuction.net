using RocketseatAuction.API.Communication.Request;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.Services.Offers.CreateOffer
{
    public class CreateOfferUseCase
    {

        private readonly ILoggedUser _loggedUser;
        private readonly IOfferRepository _repository;

        public CreateOfferUseCase(ILoggedUser loggedUser, IOfferRepository repository)
        {
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public int Execute(int ItemId, RequestCreateOfferJson request)
        {
            var loggedUser = _loggedUser.User();

            var offer = new Offer
            {
                CreatedOn = DateTime.Now,
                Price = request.Price,
                ItemId = ItemId,
                UserId = loggedUser.Id
            };

            _repository.Add(offer);

            return offer.Id;
        }
    }
}
