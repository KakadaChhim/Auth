using Authentication.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    public class AuthController : ShareController<AuthLogic>
    {
        public AuthController(IServiceProvider provider) : base(provider)
        {
        }
    }
}
