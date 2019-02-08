using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace THY.Web.Models
{
    public class RSSFeed
    {
        public string FeedParentPage { get; set; }

        public string LastBuildDate { get; set; }

        public string CurrentPageUrl { get; set; }

        public IEnumerable<RSSFeedItem> FeedItems { get; set; }

        public RSSFeed(string feedParentPage, string lastBuildDate, string currentPageUrl, IEnumerable<RSSFeedItem> feedItems)
        {
            FeedParentPage = feedParentPage;
            LastBuildDate = lastBuildDate;
            CurrentPageUrl = currentPageUrl;
            FeedItems = feedItems;
        }
    }
}