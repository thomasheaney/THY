using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGR.Web.Models
{
    public class SearchResultsPage
    {
        public BlogSearchResults Results { get; set; }
        public HtmlMeta PageInfo { get; set; }
        public double Weight { get; set; }

        public SearchResultsPage(HtmlMeta pageInfo, BlogSearchResults results)
        {
            PageInfo = pageInfo;
            Results = results;
        }
    }
}