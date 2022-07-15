using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUITsAPIs.Core.IRepositorys.IConfigration;
using SUITsAPIs.Models;
using SUITsAPIs.Models.JWT_Helper_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = model.Password,
            };

            var resalt = await _unitOfWork.AuthService.RegisterAsync(model);
            if (!resalt.IsAuthenticated)
                return BadRequest(resalt.Message);



            return Ok(resalt);
        }
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            var result = new AuthModel();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            result = await _unitOfWork.AuthService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
