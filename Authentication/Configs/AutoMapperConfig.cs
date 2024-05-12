using Authentication.Domain;
using Authentication.Model;
using AutoMapper;

namespace Authentication.Configs
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Branch, BranchListModel>().ReverseMap();
            CreateMap<Branch, BranchAddModel>().ReverseMap();
            CreateMap<Branch, BranchEditModel>().ReverseMap();
            CreateMap<Branch, BranchViewModel>().ReverseMap();

            CreateMap<User, UserListModel>().ReverseMap();
            CreateMap<User, UserAddModel>().ReverseMap();
            CreateMap<User, UserEditModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
