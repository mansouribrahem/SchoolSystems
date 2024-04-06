using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolSystems.APIs.DAL;
using SchoolSystems.APIs.DAL.UserRepo;
using SchoolSystems.APIs.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolSystems.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepo _userRepo;
        public AccountController(UserManager<ApplicationUser> userManager,IUserRepo userRepo)
        {
            _userManager=userManager;
            _userRepo=userRepo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult>  Register(RegisterDto newUser)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result=await _userRepo.Register(newUser);
                if(result.Succeeded)
                {
                    return Ok("User was added successfully!");
                }
                return BadRequest(new {errors= result.Errors } );
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userLoginDto)
        {
            if(ModelState.IsValid)
            {
               JwtSecurityToken token =await _userRepo.Login(userLoginDto);

                if (token == null)
                    return Unauthorized();
                return Ok(new
                {
                    token=new JwtSecurityTokenHandler().WriteToken(token),
                    expiration=token.ValidTo
                });

            }
            return BadRequest(ModelState);
        }

    }
}
