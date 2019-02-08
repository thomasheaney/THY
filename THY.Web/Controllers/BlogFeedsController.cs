using THY.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.PublishedContentModels;

namespace THY.Web.Controllers
{
    public class BlogFeedsController : Controller
    {

        // GET: BlogFeeds
        public ActionResult Rss()
        {
            UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            const int NUMBER_OF_FEED_ITEMS = 10;
            const string DATE_FORMAT = "ddd, dd MMM yyyy hh:mm:ss zzz";
            const string ARTICLE_TITLE_PROPERTY_ALIAS = "pageTitle";
            const string ARTICLE_AUTHOR_ALIAS = "CreatorName";
            const string ARTICLE_DATE_PROPERTY_ALIAS = "publishedDate";
            const int CONTENT_PREVIEW_LENGTH = 500;

            var currentPage = Request.Url.ToString();
            List<RSSFeedItem> results = new List<RSSFeedItem>();
            IPublishedContent homePage = umbracoHelper.TypedContentAtRoot().FirstOrDefault(x => x.ContentType.Alias.Equals("home"));
            IPublishedContent blogPage = homePage.Children.Where(x => x.DocumentTypeAlias == "blogHome").FirstOrDefault();

            IList<BlogPost> feedItems = blogPage.Descendants<BlogPost>().OrderByDescending(x => x.UpdateDate).Take(NUMBER_OF_FEED_ITEMS).ToList();

            DateTime lastBuildDate = feedItems.Max(x => x.UpdateDate);
            string siteUrl = homePage.UrlWithDomain();
            string feedUrl = Request.Url.PathAndQuery;

            var feedParentUrl = blogPage.UrlWithDomain();

            foreach (var page in feedItems)
            {
                // Obsolete. See https://skrift.io/articles/archive/strongly-typed-models-in-the-umbraco-grid/
                string articleDescription = umbracoHelper.Truncate(umbraco.library.StripHtml(page.GetGridHtml("gridEditor", "bootstrap3").ToString()), CONTENT_PREVIEW_LENGTH).ToString().Replace("&hellip;", "...");

                string title = page.HasProperty(ARTICLE_TITLE_PROPERTY_ALIAS) ? page.GetPropertyValue<string>(ARTICLE_TITLE_PROPERTY_ALIAS) : page.Name;
                string author = !string.IsNullOrEmpty(page.CreatorName)  ? page.CreatorName : "Guest";
                string link = umbracoHelper.NiceUrlWithDomain(page.Id);

                string publishedDate = ((DateTime)page.GetPropertyValue(ARTICLE_DATE_PROPERTY_ALIAS)).ToString(DATE_FORMAT);

                string permaLink = page.UrlWithDomain();
                List<string> categoryList = new List<string>();
                if (page.HasProperty("category"))
                {
                    foreach( var item in page.Category)
                    {
                        categoryList.Add(item);
                    }
                }


                results.Add(new RSSFeedItem(articleDescription, title, author, link, categoryList, publishedDate, permaLink));
            }

            RSSFeed viewModel = new RSSFeed(feedParentUrl, lastBuildDate.ToString(DATE_FORMAT), currentPage, results);
            

            return View(viewModel);
        }
    }
}