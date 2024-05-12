using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Authentication.Model;

namespace Authentication.Extensions
{
    public static class QueryParamExtension
    {
        public static IQueryable<T> ToFilterView<T>(this IQueryable<T> query, QueryParamModel filter)
        {
            // revalidate all parameter setting.
            //filter.ValidateParam();
            // filter 
            query = Filter(query, filter.Filters);

            // sort
            if (filter.Sorts != null) // && filter.Sorts.Any())
            {
                var sorts = filter.Sorts.Split(";,".ToCharArray())
                     .Where(x => !string.IsNullOrEmpty(x))
                     .Select(x => new ShareModel.Sort
                     {
                         Field = x.Replace("+", "").Replace("-", ""),
                         Dir = x.Contains("-") ? "desc" : "asc"
                     });

                query = Sort(query, sorts);
            }

            // EF does not apply skip and take without order
            query = Limit(query, filter);

            // return the final query
            return query;
        }

        public static async Task UpdateCountAsync<T>(this QueryParamModel filter, IQueryable<T> queryable)
        {
            if (filter.Filters == null)
            {
                filter.Filters = new List<Filter>();
            }
            var filtered = Filter(queryable, filter.Filters);
            filter.RowCount = await filtered.CountAsync();
            filter.PageCount = (int)Math.Ceiling((double)filter.RowCount / filter.PageSize);
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> queryable, List<Filter> filterParams)
        {
            foreach (var filter in filterParams.Where(x => !x.Manual))
            {
                if (filter != null)
                {
                    var filters = GetAllFilters(filter);
                    var values = filters.Select(f => f.Value.ToLower()).ToArray();
                    var where = Transform(filter, filters);
                    queryable = queryable.Where(where, values);
                }
            }
            return queryable;
        }

        private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<ShareModel.Sort> sort)
        {
            if (sort != null && sort.Any())
            {
                var ordering = string.Join(",",
                  sort.Select(s => $"{s.Field} {s.Dir}"));
                return queryable.OrderBy(ordering);
            }
            return queryable;
        }

        private static IQueryable<T> Limit<T>(IQueryable<T> queryable, QueryParamModel filter)
        {
            var limit = filter.PageSize;
            var offset = (filter.PageIndex - 1) * filter.PageSize;
            return queryable.Skip(offset).Take(limit);
        }

        private static readonly IDictionary<string, string>
            Operators = new Dictionary<string, string>
            {
                {"eq", "="},
                {"neq", "!="},
                {"lt", "<"},
                {"lte", "<="},
                {"gt", ">"},
                {"gte", ">="},
                {"startswith", "StartsWith"},
                {"endswith", "EndsWith"},
                {"contains", "Contains"},
                {"doesnotcontain", "Contains"},
            };

        public static IList<Filter> GetAllFilters(Filter filter)
        {
            var filters = new List<Filter>();
            GetFilters(filter, filters);
            return filters;
        }

        private static void GetFilters(Filter filter, IList<Filter> filters)
        {
            if (filter.Filters != null && filter.Filters.Any())
            {
                foreach (var item in filter.Filters)
                {
                    GetFilters(item, filters);
                }
            }
            else
            {
                filters.Add(filter);
            }
        }

        public static string Transform(Filter filter, IList<Filter> filters)
        {
            if (filter.Filters != null && filter.Filters.Any())
            {
                return "(" + string.Join(" " + filter.Logic + " ",
                    filter.Filters.Select(f => Transform(f, filters)).ToArray()) + ")";
            }
            int index = filters.IndexOf(filter);
            if (!Operators.ContainsKey(filter.Operator))
            {
                throw new ArgumentException($"Operator {filter.Operator} not support for filter!");
            }
            var comparison = Operators[filter.Operator];
            if (filter.Operator == "doesnotcontain")
            {
                //  return String.Format("({0} != null && !{0}.ToString().{1}(@{2}))",
                return string.Format("(!{0}.{1}(@{2}))",
                    filter.Field, comparison, index);
            }
            if (comparison == "StartsWith" ||
                comparison == "EndsWith" ||
                comparison == "Contains")
            {
                //return String.Format("({0} != null && {0}.ToString().{1}(@{2}))",
                return string.Format("({0}.ToLower().{1}(@{2}))",
                filter.Field, comparison, index);
            }
            return string.Format("{0} {1} @{2}", filter.Field, comparison, index);
        }
    }
}
