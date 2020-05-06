using System;
using Microsoft.AspNetCore.Mvc;
using Password.Domain.Interfaces;
using Password.Application.Models;

namespace PasswordApplication.Api.Controllers
{
    [ApiController]
    [Route("password")]
    public class PasswordController : ControllerBase
    {
        #region Constructors, Fields, Properties
        private readonly IPassword _passwordService;

        public PasswordController(IPassword passwordService)
        {
            _passwordService = passwordService;
        }

        #endregion

        [HttpPost("is-valid")]
        public ActionResult IsValid([FromBody] PasswordModel passwordObj)
        {
            var ret = _passwordService.IsValid(passwordObj.Password);
            return Ok(new ResponseModel(){ status = ret });
        }
    }
}
