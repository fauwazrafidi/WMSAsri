using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Server.Data;
using SHARED.Contracts;
using SHARED.DTOs;
using System.Text;
using static SHARED.DTOs.ServiceResponses;

namespace Server.Repositories
{
    public class AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config): IUserAccount
    {
        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            if (userDTO == null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationUser()
            {
                PasswordHash = userDTO.Password,
                UserName = userDTO.UserName,
            };
            var user = await userManager.FindByNameAsync(userDTO.UserName);
            if (user is not null) return new GeneralResponse(false, "User Register Already");

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error Occured... Please Try Again");

            //Assign Default Role : Admin to first register, rest is user;
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container si empty");

            var getUser = await userManager.FindByNameAsync(loginDTO.UserName);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPassword = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPassword)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.UserName, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login Completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
