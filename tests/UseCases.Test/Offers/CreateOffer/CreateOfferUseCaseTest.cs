using Bogus;
using FluentAssertions;
using Moq;
using RocketseatAuction.API.Communication.Request;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Services;
using RocketseatAuction.API.Services.Offers.CreateOffer;
using Xunit;

namespace UseCases.Test.Offers.CreateOffer
{
    
    public class CreateOfferUseCaseTest
    {
        [Fact]
        public void Success()
        {
            // Arrange
            var request = new Faker<RequestCreateOfferJson>()
                .RuleFor(request => request.Price, f => f.Random.Decimal(1, 1700)).Generate(); ;

            var offerRepository = new Mock<IOfferRepository>();
            var loggedUser = new Mock<ILoggedUser>();
            loggedUser.Setup(i => i.User()).Returns(new User());

            var useCase = new CreateOfferUseCase(loggedUser.Object, offerRepository.Object);

            // Act
            var act = () => useCase.Execute(0, request);

            // Assert
            act.Should().NotThrow();
        }
    }
}
