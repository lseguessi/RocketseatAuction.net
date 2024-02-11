using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.Contracts
{
    public interface IUserRepository
    {
        bool ExistUserWithEmail(string email);
        User User(string email);
    }
}
