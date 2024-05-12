using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController<TLogic> : ControllerBase where TLogic : class
    {
        public readonly DbContext _db;
        public readonly IMapper _mapper;
        public readonly TLogic _logic;
        public readonly IServiceProvider _provider;
        public ShareController(IServiceProvider provider)
        {
            _db = provider.GetRequiredService<DbContext>();
            _mapper = provider.GetRequiredService<IMapper>();
            _logic = provider.GetRequiredService<TLogic>();
            _provider = provider;
        }
    }
}
