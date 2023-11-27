using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySqlIdentity.ViewModels;

namespace MySqlIdentity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
       
        private readonly ILogger<IndexController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexController(ILogger<IndexController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IdentityResult?> Register([FromBody] RegisterViewModel model)
        {
          

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };

                return await _userManager.CreateAsync(user, model.Password);

               
            }

            return null;
        }
    }
}