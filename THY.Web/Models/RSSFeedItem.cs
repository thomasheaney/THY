using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THY.Web.Models
{
    public class RSSFeedItem
    {

        public string ArticleDescription { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string PublishedDate { get; set; }
        public string PermaLink { get; set; }
        public string Author { get; set; }
        public List<string> CategoryList { get; set; }

        public RSSFeedItem(string articleDescription, string title, string author, string link, List<string> categoryList, string publishedDate, string permaLink)
        {
            ArticleDescription = articleDescription;
            Title = title;
            Author = author;
            Link = link;
            CategoryList = categoryList;
            PublishedDate = publishedDate;
            PermaLink = permaLink;
        }
    }
}