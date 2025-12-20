using System;
using OpticianWebAPI.DTOs;


namespace OpticianWebAPI.Services.abstracts
{
    public interface IAuthService
    {
        public string? Login(LoginRequest loginRequest);

    }
}