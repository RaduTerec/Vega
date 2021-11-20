using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vega.Controllers.DataTransferObjects;
using Vega.Core.Models;

namespace Vega.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string DefaultRole = "User";
        private readonly UserManager<VegaUser> _userManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        public UserController(UserManager<VegaUser> userManager, IMapper mapper, IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            var user = _mapper.Map<VegaUser>(userDto);

            var userWithSameEmail = await _userManager.FindByEmailAsync(userDto.Email);
            if (userWithSameEmail != null)
            {
                return BadRequest($"User with {userDto.Email} is already registered");
            }

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Something went wrong");
            }

            await _userManager.AddToRoleAsync(user, DefaultRole);

            var jwtSecurityToken = await CreateJwtToken(user);
            var authResponse = new AuthenticationResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            return Ok(authResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return BadRequest($"Could not authenticate {login.Email}");
            }

            if (await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var jwtSecurityToken = await CreateJwtToken(user);
                var authResponse = new AuthenticationResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };

                return Ok(authResponse);
            }

            return BadRequest($"Could not authenticate {login.Email}");
        }

        private async Task<JwtSecurityToken> CreateJwtToken(VegaUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}