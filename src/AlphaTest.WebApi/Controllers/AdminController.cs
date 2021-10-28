﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaTest.WebApi.Models.Admin.UserManagement;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Admin.Commands.UserManagement.CreateUser;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public AdminController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateNewUser(CreateUserRequest request)
        {

            Guid userID = await _alphaTest.ExecuteUseCaseAsync(
                new CreateUserUseCaseRequest(
                    request.FirstName, 
                    request.LastName, 
                    request.MiddleName, 
                    request.Email, 
                    request.TemporaryPassword, 
                    request.InitialRole));
            return Ok(userID);
        }
    }
}