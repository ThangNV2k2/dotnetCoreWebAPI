﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyApiNet6.data;
using MyApiNet6.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApiNet6.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return String.Empty;
            }
            var authenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT: ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authenClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser 
            { 
                FristName = model.FristName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            return await userManager.CreateAsync(user, model.Password);
        }
    }
}
