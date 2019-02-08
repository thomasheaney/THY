using THY.Web.Models;
using THY.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedContentModels;
using X.PagedList;
using THY.Web.ViewModels;

namespace THY.Web.Controllers
{
    public class BlogSearchController : Controller
    {
        private BlogResultsHelper blogResultsHelper = new BlogResultsHelper();
        private readonly UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

        private readonly int PAGESIZE = 10;

        private string PartialViewPath(string name)
        {
            return $"/Views/Partials/BlogSearch/{name}.cshtml";
        }

        public ActionResult Category(string category, int? page)
        {
            
            List<BlogPreview> blogResults = new List<BlogPreview>();

            var pageNumber = page ?? 1;

            if (!string.IsNullOrEmpty(category))
            {
                blogResults = blogResultsHelper.getResultsByCategory(category);
            }
            var urlSegment = string.Format("/blog/category/{0}", category);
            var pageInfo = new HtmlMeta("SITE NAME", category, "", "http://www.example.com", "@OGSiteName", "@TwitterName");
            BlogSearchResults searchResults = new BlogSearchResults(category, "Categorised as", urlSegment, blogResults.ToPagedList(pageNumber, PAGESIZE));

            var viewModel = new SearchResultsViewModel
            {
                PageInfo = pageInfo,
                Results = searchResults
            };
            return View(viewModel);
        }

        public ActionResult Tags(string tag, int? page)
        {
            List<BlogPreview> blogResults = new List<BlogPreview>();

            var pageNumber = page ?? 1;

            if (!string.IsNullOrEmpty(tag))
            {
                //model = getExamineSearchResultsByField("tagsIndexed", tag);
                blogResults = blogResultsHelper.getResultsByTag(tag);
            }

            var urlSegment = string.Format("/blog/tags/{0}", tag);
            var pageInfo = new HtmlMeta("SITE NAME", tag, "", "http://www.example.com", "@OGSiteName", "@TwitterName");

            BlogSearchResults searchResults = new BlogSearchResults(tag, "Tagged with", urlSegment, blogResults.ToPagedList(pageNumber, PAGESIZE));

            var viewModel = new SearchResultsViewModel
            {
                PageInfo = pageInfo,
                Results = searchResults
            };

            return View(viewModel);
        }

        public ActionResult ArchiveByYearMonth(int year, int month, int? page)
        {

            IPublishedContent homePage = umbracoHelper.TypedContentAtRoot().FirstOrDefault(x => x.ContentType.Alias.Equals("home"));
            IPublishedContent blogPage = homePage.Children.Where(x => x.DocumentTypeAlias == "blogHome").FirstOrDefault();

            var pageNumber = page ?? 1;

            List<BlogPost> blogResults = blogPage.Descendants<BlogPost>().ToList();
            List<BlogPreview> blogPreviews = new List<BlogPreview>();
            foreach (BlogPost post in blogResults.Where(r => r.CreateDate.Year == year && r.CreateDate.Month == month).OrderByDescending(x => x.CreateDate))
            {
                int imageId = post.GetPropertyValue<int>("summaryImage");
                var mediaItem = umbracoHelper.Media(imageId);

                blogPreviews.Add(new BlogPreview(post.Name, post.SummaryText, mediaItem.Url, post.Url, post.CreateDate, post.CreatorName));
            }

            var date = new DateTime(year, month, 1);
            var urlSegment = string.Format("/blog/{0}/{1}", date.ToString("yyyy"), date.ToString("MM"));

            var infoTitle = date.ToString("MM yyyy") + " Archives";
            var pageInfo = new HtmlMeta("SITE NAME", infoTitle, "", "http://www.example.com", "@OGSiteName", "@TwitterName");

            BlogSearchResults searchResults = new BlogSearchResults(date.ToString("MMMM yyyy"), "Archives", urlSegment, blogPreviews.ToPagedList(pageNumber, PAGESIZE));

            var viewModel = new SearchResultsViewModel
            {
                PageInfo = pageInfo,
                Results = searchResults
            };
            return View(viewModel);
        }

        public ActionResult Search(string blogSearchString, int? page)
        {
            List<IPublishedContent> contents = new List<IPublishedContent>();
            List<BlogPreview> results = new List<BlogPreview>();
            var pageNumber = page ?? 1;

            var searcher =  "BlogSearcher";

            //Fetching our SearchProvider by giving it the name of our searchprovider
            var Searcher = Examine.ExamineManager.Instance.SearchProviderCollection[searcher];

            foreach (var item in umbracoHelper.TypedSearch(blogSearchString, false, "BlogSearcher"))
            {
                var post = umbracoHelper.TypedContent(item.Id);

                int imageId = post.GetPropertyValue<int>("summaryImage");
                var mediaItem = umbracoHelper.Media(imageId);
                results.Add(new BlogPreview(post.Name, post.GetPropertyValue<string>("summaryText"), mediaItem.Url, post.Url, post.CreateDate, post.CreatorName, 0));
            }

            var urlSegment = string.Format("/blog/search/{0}", blogSearchString);
            var pageInfo = new HtmlMeta("SITE NAME", "Search results for " + blogSearchString , "", "http://www.example.com", "@OGSiteName", "@TwitterName");

            BlogSearchResults searchResults = new BlogSearchResults(blogSearchString, "Search", urlSegment, results.ToPagedList(pageNumber, PAGESIZE));

            var viewModel = new SearchResultsViewModel
            {
                PageInfo = pageInfo,
                Results = searchResults
            };

            return View(viewModel);
        }

        public ActionResult renderTagCloud()
        {
            // Tag size calculation taken from 
            //https://stackoverflow.com/questions/3717314/what-is-the-formula-to-calculate-the-font-size-for-tags-in-a-tagcloud
            List<TagListItem> model = new List<TagListItem>();
            IEnumerable<TagModel> allTags = umbracoHelper.TagQuery.GetAllTags().OrderByDescending<TagModel, int>(x => x.NodeCount);

            var max = allTags.Max(m => m.NodeCount);
            var min = allTags.Min(m => m.NodeCount);

            double fontMax = 5;
            double fontMin = 1;

            foreach (var item in allTags)
            {
                double size = item.NodeCount == min 
                    ? fontMin
                    : (item.NodeCount / (double)max) * (fontMax - fontMin) + fontMin;

                model.Add(new TagListItem(item.Text, item.NodeCount, size));
            }

            return PartialView(PartialViewPath("_TagCloud"), model);
        }

        public ActionResult renderCategoryList()
        {
            var model = new List<string>();
            var dts = umbracoHelper.DataTypeService;
            var dataType = dts.GetDataTypeDefinitionByName("Blog Categories");
            var preValuesFromDataType = dts.GetPreValuesCollectionByDataTypeId(dataType.Id).FormatAsDictionary();

            foreach(var item in preValuesFromDataType.Values)
            {
                model.Add(item.Value);
            }

            model.Sort();
            return PartialView(PartialViewPath("_CategoryList"), model);
        }

        public ActionResult renderArchiveList(bool asTree = false)
        {
            var model = new List<ArchiveYear>();
            IPublishedContent homePage = umbracoHelper.TypedContentAtRoot().FirstOrDefault(x => x.ContentType.Alias.Equals("home"));
            IPublishedContent blogPage = homePage.Children.Where(x => x.DocumentTypeAlias == "blogHome").FirstOrDefault();

            List<BlogPost> results = blogPage.Descendants<BlogPost>().ToList();

            foreach (BlogPost page in results.OrderByDescending(x=> x.CreateDate))
            {

                var publishDate = (page.CreateDate != DateTime.MinValue && page.CreateDate != null) ? page.CreateDate : page.CreateDate;

                var yearInList = model.Find(x => x.Year == publishDate.Year);
                if (yearInList == null)
                {
                    yearInList = new ArchiveYear(publishDate.Year, new List<ArchiveMonth>());
                    model.Add(yearInList);
                    yearInList = model.Find(x => x.Year == publishDate.Year);
                }

                var monthInList = yearInList.Months.Find(x => x.Month == publishDate.Month);
                if (monthInList == null)
                {
                    monthInList = new ArchiveMonth(publishDate.Month, publishDate.ToString("MMMM"), new Dictionary<string, string>());
                    monthInList.ArticleLinks.Add(page.Name, page.Url);
                    yearInList.Months.Add(monthInList);
                }
                else
                {
                    monthInList.ArticleLinks.Add(page.Name, page.Url);
                }
            }

            model.OrderBy(x => x.Year);

            foreach ( var year in model)
            {
                year.Months.OrderBy(x => x.Month);
                var tempCounter = 0;
                foreach (var month in year.Months)
                {
                    month.ArticleCount = month.ArticleLinks.Count();
                    tempCounter = month.ArticleCount;
                }

                year.ArticleCount = tempCounter;
            }

            return asTree ? PartialView(PartialViewPath("_ArchiveTreeList"), model)  : PartialView(PartialViewPath("_ArchiveList"), model);
        }


    }
}