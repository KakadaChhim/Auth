using Authentication.Domain;
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
        public void UpdateField<TDomain, TModel>(TDomain entry, TModel model) where TDomain : ShareDomain, new()
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                // has column in model but not have in entity
                if (entry.GetType().GetProperty(prop.Name) == null)
                {
                    continue;
                }
                // same column different type.
                if (prop.PropertyType != entry.GetType().GetProperty(prop.Name).PropertyType)
                {
                    continue;
                }

                entry.GetType().GetProperty(prop.Name).SetValue(entry, prop.GetValue(model, null));
            }
            entry.LastModifiedDate = DateTime.Now;
        }
    }
}
