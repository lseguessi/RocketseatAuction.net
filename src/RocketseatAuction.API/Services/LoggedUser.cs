using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Services
{
    public class LoggedUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public LoggedUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public User User()
        {
            var repository = new RocketseatAuctionDbContext();

            var token = TokenOnRequest();
            var email = FromBase64String(token);

            return repository.Users.First(user => user.Email.Equals(email));
        }

        private string TokenOnRequest()
        {
            var authentication = _httpContext.HttpContext!.Request.Headers.Authorization.ToString();

            return authentication["Bearer ".Length..];
        }

        private string FromBase64String(string base64String)
        {
            var data = Convert.FromBase64String(base64String);

            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}
