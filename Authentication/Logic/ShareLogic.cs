using AutoMapper;

namespace Authentication.Logic
{
    public class ShareLogic
    {
        protected readonly DbContext _db;
        protected readonly IServiceProvider _provider;
        protected readonly IMapper _mapper;
        public ShareLogic(IServiceProvider provider)
        {
            _provider = provider;
            _db = provider.GetRequiredService<DbContext>();
            _mapper = provider.GetRequiredService<IMapper>();
        }
    }
}
