using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.PagedList;

namespace THY.Web.Models
{
    public class BlogSearchResults
    {
        public string SearchString { get; set; }

        public string HeadingText { get; set; }

        public string UrlSegment { get; set; }

        public IPagedList<BlogPreview> Results { get; set; }

        public BlogSearchResults(string searchString, string headingText, string urlSegment, IPagedList<BlogPreview> results)
        {
            SearchString = searchString;
            HeadingText = headingText;
            UrlSegment = urlSegment;
            Results = results;

        }
    }
}