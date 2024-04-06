using Microsoft.AspNetCore.Identity;
using SchoolSystems.APIs.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolSystems.APIs.DAL.UserRepo
{
    public interface IUserRepo
    {
        public Task<IdentityResult>  Register(RegisterDto newUser);
        public Task<JwtSecurityToken> Login(LoginDto userLoginDto);
    }
}
