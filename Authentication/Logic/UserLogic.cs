using Authentication.Domain;
using Authentication.Helper;
using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using static Authentication.Model.ShareModel;

namespace Authentication.Logic
{
    public class UserLogic: ShareLogic
    {
        public UserLogic(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<ShareModel.SearchResult<UserListModel, QueryParamModel>> SearchAsyns(QueryParamModel param)
        {
            var entity = _db.Users.Where(x => x.Active);
            return new ShareModel.SearchResult<UserListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<UserListModel>>(await entity.ToListAsync()),
                Param = param
            };
        }

        public async Task<UserViewModel> FindAsync(long id)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) { throw new NotFoundException("Cannot find user!"); }
            return _mapper.Map<UserViewModel>(entity);
        }

        public async Task<UserViewModel> AddAsync(UserAddModel model)
        {
            var entity = _mapper.Map<User>(model);
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }

        public async Task<UserViewModel> RemoveAsync(RemoveModel model)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (entity == null) { throw new NotFoundException("Cannot find user"); }
            entity.Active = false;
            await _db.SaveChangesAsync();
            return _mapper.Map<UserViewModel>(entity);
        }
    }
}
