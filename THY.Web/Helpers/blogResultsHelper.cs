using THY.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;


namespace THY.Web.Helpers
{
    public class BlogResultsHelper
    {
        private readonly UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        private PresentationHelper presentationHelper = new PresentationHelper();

        public List<BlogPreview> getResultsByTag(string tagName)
        {
            List<BlogPreview> results = new List<BlogPreview>();
            var tagResults = umbracoHelper.TagQuery.GetContentByTag(tagName);

            foreach (IPublishedContent page in tagResults)
            {

                if (page.GetPropertyValue<IEnumerable<string>>("tags").Contains(tagName))
                {
                    results.Add(new BlogPreview(page.Name, 
                        page.GetPropertyValue<string>("summaryText"), 
                        presentationHelper.GetUmbracoMediaUrl(page, "summaryImage"), 
                        page.Url, 
                        page.CreateDate, 
                        page.CreatorName, 
                        0));
                }

            }

            return (results);
        }

        public List<BlogPreview> getResultsByCategory(string category)
        {
            List<BlogPreview> results = new List<BlogPreview>();
            IPublishedContent homePage = umbracoHelper.TypedContentAtRoot().FirstOrDefault(x => x.ContentType.Alias.Equals("home"));
            IPublishedContent blogPage = homePage.Children.Where(x => x.DocumentTypeAlias == "blogHome").FirstOrDefault();

            foreach (IPublishedContent page in blogPage.Children.OrderByDescending(x => x.UpdateDate))
            {

                if (page.GetPropertyValue<IEnumerable<string>>("Category").Contains(category))
                {
                    results.Add(new BlogPreview(page.Name, 
                        page.GetPropertyValue<string>("summaryText"), 
                        presentationHelper.GetUmbracoMediaUrl(page, "summaryImage"), 
                        page.Url, 
                        page.CreateDate, 
                        page.CreatorName, 
                        0));
                }

            }

            return (results);
        }

        public List<BlogPreview> getTopResults(int noOfResults)
        {
            List<BlogPreview> results = new List<BlogPreview>();
            IPublishedContent homePage = umbracoHelper.TypedContentAtRoot().FirstOrDefault(x => x.ContentType.Alias.Equals("home"));
            IPublishedContent blogPage = homePage.Children.Where(x => x.DocumentTypeAlias == "blogHome").FirstOrDefault();

            //results = blogPage.Descendants<BlogPost>().ToList();

            foreach (IPublishedContent page in blogPage.Children.OrderByDescending(x => x.UpdateDate).Take(noOfResults))
            {
                results.Add(new BlogPreview(page.Name, 
                    page.GetPropertyValue<string>("summaryText"), 
                    presentationHelper.GetUmbracoMediaUrl(page, "summaryImage"), 
                    page.Url, page.CreateDate, 
                    page.CreatorName, 
                    0));
            }

            return (results);
        }

        public List<BlogPreview> getExamineSearchResultsByField(string searchField, string searchString, string searcher = "")
        {
            List<IPublishedContent> contents = new List<IPublishedContent>();
            List<BlogPreview> results = new List<BlogPreview>();

            // Use the BlogSearcher if no other is supplied. If this method is made into a helper then
            // change to use the default searcher.
            searcher = !string.IsNullOrEmpty(searcher) ? searcher : "BlogSearcher";

            //Fetching our SearchProvider by giving it the name of our searchprovider
            var Searcher = Examine.ExamineManager.Instance.SearchProviderCollection[searcher];

            //Searching and ordering the result by score, and we only want to get the results that has a minimum of 0.05(scale is up to 1.)
            // var searchResults = Searcher.Search(q, true).OrderByDescending(x => x.Score).TakeWhile(x => x.Score > 0.05f);

            var searchCriteria = Searcher.CreateSearchCriteria(Examine.SearchCriteria.BooleanOperation.Or);
            var query = searchCriteria.Field(searchField, searchString);

            //Searching and ordering the result by score, and we only want to get the results that has a minimum of 0.05(scale is up to 1.)
            var searchResults = Searcher.Search(query.Compile()).OrderByDescending(x => x.Score).TakeWhile(x => x.Score > 0.05f);

            // Build a list of IPublishedContent objects using an Umbraco helper
            foreach (var item in searchResults)
            {
                var page = umbracoHelper.TypedContent(item.Id);

                results.Add(new BlogPreview(page.Name, 
                    page.GetPropertyValue<string>("summaryText"), 
                    presentationHelper.GetUmbracoMediaUrl(page, "summaryImage"), 
                    page.Url, 
                    page.CreateDate, 
                    page.CreatorName, 
                    0));
            }

            return (results);
        }
    }
}