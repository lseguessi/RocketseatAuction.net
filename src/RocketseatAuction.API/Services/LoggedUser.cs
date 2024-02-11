﻿using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.Services
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserRepository _repository;

        public LoggedUser(IHttpContextAccessor httpContext, IUserRepository repository)
        {
            _httpContext = httpContext;
            _repository = repository;
        }

        public User User()
        {
            var token = TokenOnRequest();
            var email = FromBase64String(token);

            return _repository.User(email);
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
