using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNet6.Models;
using MyApiNet6.Repositories;

namespace MyApiNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository AccRepo;

        public AccountsController(IAccountRepository repo) 
        {
            AccRepo = repo;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await AccRepo.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await AccRepo.SignInAsync(model);
            if(string.IsNullOrEmpty(result))
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
