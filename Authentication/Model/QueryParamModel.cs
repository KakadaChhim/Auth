using Authentication.Configs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Authentication.Model
{
    [ModelBinder(BinderType = typeof(QueryParamBinder))]
    public class QueryParamModel
    {
        public QueryParamModel()
        {
            PageCount = PageIndex;
        }

        [DefaultValue(1)]
        [Description("Current page number of results (1-based index).")]
        [FromQuery(Name = "pageIndex")]
        public int PageIndex { get; set; }
        [DefaultValue(25)]
        [Description("Page size or number of records per page")]
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }
        [Description("Array list of field (+asc, -desc) eg. Name,Sex-,Age+ ")]
        public string Sorts { get; set; }

        [Description("JSON List of filter. eg. {'field':'Name','operator':'contains','value':'ja'} ")]
        [FromQuery(Name = "filters")]
        public List<Filter> Filters { get; set; }
        public int PageCount { get; set; }
        public int RowCount { get; set; }
        
        public long GetManualFilterLong(string key)
        {
            try{
                return long.Parse(GetManualFilter(key));
            }
            catch
            {
                return 0;
            } 
        }
        
        public string GetManualFilter(string key)
        {
            var value = "";
            var filter = Filters.FirstOrDefault(x => x.Field.ToLower() == key.ToLower());
            if (filter != null)
            {
                filter.Manual = true;
                value = filter.Value;
            }
            return value;
        }
        public string GetSearchFilter()
        {
            var search = "";
            var searchFilter = Filters.FirstOrDefault(x => x.Field.ToLower() == "search");
            if (searchFilter != null)
            {
                search = searchFilter.Value;
                searchFilter.Manual = true;
            }
            return search;
        }

    }
    public class Filter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string Logic { get; set; }
        public bool Manual { get; set; }
        public IEnumerable<Filter> Filters { get; set; }
    }
}
