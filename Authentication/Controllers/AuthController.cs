using Authentication.Logic;
using Authentication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    public class AuthController : ShareController<AuthLogic>
    {
        public AuthController(IServiceProvider provider) : base(provider)
        {
        }
        [HttpPost("login")]
        public async Task<string> PostAsync(AuthModel model)
        {
            return await  _logic.Login(model);
        }
    }
}
