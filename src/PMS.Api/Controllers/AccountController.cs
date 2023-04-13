﻿using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.Requests.Account;

namespace PMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;



        public AccountController(IUserService userService)
        {
            _userService = userService;
        }




        [HttpPost("/Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Register request)
        {
            await _userService.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName, request.PhoneNumber);

            return Created("/Account", null);
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login request)
        {
            await _userService.LoginAsync(request.Email, request.Password);

            return Ok();
        }
    }
}