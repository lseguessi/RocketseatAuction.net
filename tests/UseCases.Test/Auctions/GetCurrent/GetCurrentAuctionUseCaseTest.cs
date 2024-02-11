using Bogus;
using FluentAssertions;
using Moq;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Enums;
using RocketseatAuction.API.UseCases.Auctions.GetCurrent;
using Xunit;

namespace UseCases.Test.Auctions.GetCurrent
{
    public class GetCurrentAuctionUseCaseTest
    {
        [Fact]
        public void Success()
        {
            // Arrange
            var testAuction = new Faker<Auction>()
                .RuleFor(auction => auction.Id, f => f.Random.Number(1, 700))
                .RuleFor(auction => auction.Name, f => f.Lorem.Word())
                .RuleFor(auction => auction.Starts, f => f.Date.Past())
                .RuleFor(auction => auction.Ends, f => f.Date.Future())
                .RuleFor(auction => auction.Items, (f, auction) => new List<Item>
                {
                    new Item {
                         Id =  f.Random.Number(1, 200),
                         Name = f.Commerce.ProductName(),
                         Brand = f.Commerce.ProductAdjective(),
                         BasePrice = f.Random.Decimal(50, 2000),
                         Condition = f.PickRandom<Condition>(),
                         AuctionId = auction.Id
                    }
                }).Generate(); ;

            var mock = new Mock<IAuctionRepository>();
            mock.Setup(i => i.GetCurrent()).Returns(testAuction);

            var useCase = new GetCurrentAuctionsUseCase(mock.Object);

            // Act
            var auction = useCase.Execute();

            // Assert
            auction.Should().NotBeNull();
            auction.Should().BeEquivalentTo(testAuction);
            auction?.Id.Should().Be(testAuction.Id);
            auction?.Name.Should().Be(testAuction.Name);
            auction?.Starts.Should().Be(testAuction.Starts);
            auction?.Ends.Should().Be(testAuction.Ends);
            auction?.Items.Should().BeEquivalentTo(testAuction.Items);

        }
    }
}
