using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THY.Web.Helpers;
using THY.Web.Models;
using Umbraco.Web.Mvc;
using X.PagedList;

namespace THY.Web.Controllers
{
    public class BlogHomeController : SurfaceController
    {
        private BlogResultsHelper blogResultsHelper = new BlogResultsHelper();
        private string PartialViewPath(string name)
        {
            return "~/Views/Partials/BlogHome/" + name + ".cshtml";
        }

        public ActionResult RenderPostList(int numberOfItems)
        {

            List<BlogPreview> blogResults = new List<BlogPreview>();

            blogResults = blogResultsHelper.getTopResults(numberOfItems);

            var urlSegment = string.Format("/blog/");
            BlogSearchResults model = new BlogSearchResults("", "", urlSegment, blogResults.ToPagedList(1, numberOfItems));

            return PartialView("~/Views/Partials/BlogSearch/_SearchResults.cshtml", model);

        }
    }
}