using Authentication.Domain;
using Authentication.Model;
using AutoMapper;

namespace Authentication.Configs
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Branch, BranchListModel>();
            CreateMap<Branch, BranchAddModel>();
            CreateMap<Branch, BranchEditModel>();
            CreateMap<Branch, BranchViewModel>();

            CreateMap<User, UserListModel>();
            CreateMap<User, UserAddModel>();
            CreateMap<User, UserEditModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}
