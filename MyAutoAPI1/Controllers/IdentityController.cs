﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Controllers.GetQueries;
using MyAutoAPI1.Services.Identity;
using System;
using System.Threading.Tasks;

namespace MyAutoAPI1.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : BaseController.BaseController
    {
        private readonly IIdentityServices _identityServices;

        public IdentityController(IIdentityServices identityServices)
        {
            _identityServices = identityServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var res = await _identityServices.RegisterAsync(request.Email, request.Password, request.Name);
            return DataResponse(res);
        }

    }
}