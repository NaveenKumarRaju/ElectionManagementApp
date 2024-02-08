using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")] 
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            Expression<Func<AppUser, bool>> userByUsername = e => e.UserName == registerDto.UserName;
            Expression<Func<AppUser, bool>> userByVoterId = e => e.UserName == registerDto.VoterId;

            if(await UserExists(userByUsername)) return BadRequest("Username already taken, please give another username");

            if(await UserExists(userByVoterId)) return BadRequest("Voter Id is already registered");

            var user = _mapper.Map<AppUser>(registerDto);
            
            user.UserName = registerDto.UserName.Trim().ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            
            if(!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto {
                    Id = user.Id,
                    Username = user.UserName,
                    FullName = user.FullName,
                    Token = await _tokenService.CreateToken(user),
                    Address = user.Address,
                    IsApproved = user.IsApproved,
                    HasVoted = user.HasVoted  
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {           
            var user = await _userManager.Users
                                    .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if(user == null) return Unauthorized("invalid username");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!result) return Unauthorized("invalid password");

            return new UserDto {
                    Id = user.Id,
                    Username = user.UserName,
                    FullName = user.FullName,
                    Token = await _tokenService.CreateToken(user),
                    Address = user.Address,
                    IsApproved = user.IsApproved,
                    HasVoted = user.HasVoted  
            };
        }

        private async Task<bool> UserExists(Expression<Func<AppUser, bool>> predicate)
        {
            return await _userManager.Users.AnyAsync(predicate);
        }
    }
}