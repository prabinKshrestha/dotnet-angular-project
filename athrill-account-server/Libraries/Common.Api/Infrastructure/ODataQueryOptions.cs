using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Common.Api.Infrastructure
{
    public class ODataQueryOptions
    {
        public ExpandClause ExpandClause;
        public SelectClause SelectClause;
        public OrderByClause OrderByClause;
        public FilterClause FilterClause;
        public string Search;
        public int? Top;
        public int? Skip;
        public bool NoPaging = false;
        public bool IsExpandDefined => ExpandClause != null && ExpandClause.Exists;
        public bool IsSelectDefined => SelectClause != null && SelectClause.Exists;
        public bool IsOrderByDefined => OrderByClause != null && OrderByClause.Exists;
        public bool IsFilterDefined => FilterClause != null && FilterClause.Exists;
    }

    #region Expand
    public class ExpandClause
    {
        public List<string> ExpandItems;
        public bool Exists => ExpandItems != null && ExpandItems.Any();
    }
    #endregion

    #region Filter
    public class FilterClause
    {
        public List<string> FilterItems;
        public bool Exists => FilterItems != null && FilterItems.Any();
    }
    #endregion

    #region Select
    public class SelectClause
    {
        public List<string> SelectItems;
        public bool Exists => SelectItems != null && SelectItems.Any();
    }
    #endregion

    #region OrderBy
    public class OrderByClause
    {
        public List<OrderByItem> OrderByItems;
        public bool Exists => OrderByItems != null && OrderByItems.Any();

        public OrderByClause() { }
        public OrderByClause(IEnumerable<OrderByItem> orderByItems)
        {
            OrderByItems = orderByItems.ToList();
        }
        // following overridden is necesasary so that we can use it in LINQ to Entity for ordering while fetching records from database
        public override string ToString()
        {
            return OrderByItems != null && OrderByItems.Any()
                                        ? string.Join(", ", OrderByItems.Select(x => $"{x.ModelProperty} {x.SortDirection.ToString().ToLower()}"))
                                        : string.Empty;

        }
        // below metthod is actuall ordering or sorting syntax for collection .
        public string ToJson()
        {
            string retVal = "";
            if(OrderByItems != null && OrderByItems.Any())
            {
                retVal = string.Join(",", OrderByItems.Select(x => $"'{x.ModelProperty}':{(x.SortDirection == SortDirection.Ascending ? 1 :-1)}"));
                retVal = "{" + retVal + "}";
            }
            return retVal;
        }
    }

    public class OrderByItem
    {
        public string ModelProperty { get; set; }
        public SortDirection SortDirection { get; set; }

        public OrderByItem() { }
        public OrderByItem(string property) 
        {
            ModelProperty = property;
            SortDirection = SortDirection.Ascending;
        }
        public OrderByItem(string property, string direction)
        {
            ModelProperty = property;
            SortDirection = SortDirection.Ascending;
            if(!string.IsNullOrEmpty(direction) && direction.Trim().StartsWith("desc", StringComparison.OrdinalIgnoreCase))
            {
                SortDirection = SortDirection.Descending;
            }
        }
        public OrderByItem(string property, SortDirection direction)
        {
            ModelProperty = property;
            SortDirection = direction;
        }
    }
    #endregion

}
