using Authentication.Domain;
using Authentication.Extensions;
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

        public async Task<SearchResult<UserListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var entities = _db.Users.Where(x => x.Active);
            var search = param.GetSearchFilter().ToLower();
            if (string.IsNullOrEmpty(param.Sorts))
            {
                param.Sorts = nameof(User.Name);
            }

            if (!string.IsNullOrEmpty(search))
            {
                entities = entities.Where(x => x.Name.ToLower().Contains(search));
            }
            await param.UpdateCountAsync(entities);
            entities = entities.ToFilterView(param);
            return new SearchResult<UserListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<UserListModel>>(await entities.ToListAsync()),
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
            entity.PasswordHash = model.Password;
            entity.PasswordSalt = model.Password;
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
