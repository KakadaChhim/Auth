using Authentication.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Authentication.Configs
{
    public class QueryParamBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var values = bindingContext.ValueProvider.GetValue("filters").FirstValue;
            var pageIndex = bindingContext.ValueProvider.GetValue("pageIndex").FirstValue;
            var pageSize = bindingContext.ValueProvider.GetValue("pageSize").FirstValue;
            var sorts = bindingContext.ValueProvider.GetValue("sorts").FirstValue;
            if (string.IsNullOrEmpty(sorts))
            {
                sorts = "";
            }
            var filters = string.IsNullOrEmpty(values) ? new List<Filter>() : JsonConvert.DeserializeObject<List<Filter>>(values);

            var result = new QueryParamModel
            {
                Filters = filters,
                PageIndex = int.Parse(pageIndex),
                PageSize = int.Parse(pageSize),
                Sorts = sorts
            };
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }
}
