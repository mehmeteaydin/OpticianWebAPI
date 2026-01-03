using System;
using OpticianWebAPI.DTOs;


namespace OpticianWebAPI.Services.abstracts
{
    public interface IAuthService
    {
        public Task<string?> Login(LoginRequest loginRequest);
        public Task<bool> RegisterUser(RegisterUserRequest registerUserRequest); 

    }
}