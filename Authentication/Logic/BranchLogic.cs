using Authentication.Domain;
using Authentication.Helper;
using Authentication.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Logic
{
    public class BranchLogic : ShareLogic
    {
        public BranchLogic(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<ShareModel.SearchResult<BranchListModel, QueryParamModel>> SearchAsyns(QueryParamModel param)
        {
            var entity = _db.Branches.Where(x => x.Active);
            return new ShareModel.SearchResult<BranchListModel, QueryParamModel> 
            { 
                Results = _mapper.Map<List<BranchListModel>>(await entity.ToListAsync()), 
                Param = param 
            };
        }

        public async Task<BranchViewModel> FindAsync(long id)
        {
            var entity = await _db.Branches.FirstOrDefaultAsync(x => x.Id == id);   
            if (entity == null) { throw new NotFoundException("Cannot find Branch"); }
            return _mapper.Map<BranchViewModel>(entity);    
        }

        public async Task<BranchViewModel> AddAsync(BranchAddModel model)
        {
            var entity = _mapper.Map<Branch>(model);
            await _db.Branches.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }

        public async Task<BranchViewModel> RemoveAsync(BranchRemove model)
        {
            var entity = await _db.Branches.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (entity == null) { throw new NotFoundException("Cannot find Branch");  }
            entity.Active = false;
            await _db.SaveChangesAsync();
            return _mapper.Map<BranchViewModel>(entity);
        }
    }
}
