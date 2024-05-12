using Authentication.Logic;
using Authentication.Model;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    public class UserController : ShareController<UserLogic>
    {
        public UserController(IServiceProvider provider) : base(provider)
        {
        }
        [HttpGet]
        public async Task<ShareModel.SearchResult<UserListModel, QueryParamModel>> SearchAsync([FromQuery] QueryParamModel param)
        {
            return await _logic.SearchAsync(param);
        }
        [HttpGet("{id}")]
        public async Task<UserViewModel> FindAsync(long id)
        {
            return await _logic.FindAsync(id);
        }

        [HttpPost()]
        public async Task<UserViewModel> PostAsync(UserAddModel model)
        {
            return await _logic.AddAsync(model);
        }
    }
}
