using Authentication.Logic;
using Authentication.Model;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    public class BranchController : ShareController<BranchLogic>
    {
        public BranchController(IServiceProvider provider) : base(provider)
        {
        }

        [HttpGet]
        public async Task<ShareModel.SearchResult<BranchListModel, QueryParamModel>> SearchAsync([FromQuery] QueryParamModel param)
        {
            return await _logic.SearchAsyns(param);
        }
        [HttpGet("{id}")]
        public async Task<BranchViewModel> FindAsync(long id)
        {
            return await _logic.FindAsync(id);
        }

        [HttpPost()]
        public async Task<BranchViewModel> PostAsync(BranchAddModel model)
        {
            return await  _logic.AddAsync(model);
        }
    }
}
