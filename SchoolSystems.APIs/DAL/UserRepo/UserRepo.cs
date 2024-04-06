using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolSystems.APIs.DTOs;
using SchoolSystems.DAL.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolSystems.APIs.DAL.UserRepo
{
    public class UserRepo:IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IConfiguration _config;
        public UserRepo(ApplicationDbContext context,UserManager<ApplicationUser> userMananger,IConfiguration config)
        {
            _context = context;
            _userManager = userMananger;
            _config = config;
        }

        public async Task<JwtSecurityToken> Login(LoginDto userLoginDto)
        {
           //check if user name exists in db 
           ApplicationUser user=await _userManager.FindByNameAsync(userLoginDto.UserName);
            if (user==null)
                return null;
            bool found= await _userManager.CheckPasswordAsync(user,userLoginDto.Password);
            if (found)
            {
                //create token [claims - sigining creds]
                //claims 
                List<Claim> userClaims=new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                //roles
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles) {
                    userClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                //signingCredentials
                SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                SigningCredentials signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    claims: userClaims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials:signingCreds
                    
                    );

                return token;    
            }
            return null;

        }

        public async Task<IdentityResult> Register(RegisterDto newUser)
        {
            ApplicationUser user= new ApplicationUser();
            
            user.UserName = newUser.UserName;
            user.Email = newUser.Mail;
            IdentityResult result=await _userManager.CreateAsync(user,newUser.Password);
            return result;

        }
    }
}
