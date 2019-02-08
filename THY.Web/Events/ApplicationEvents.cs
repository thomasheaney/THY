using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;

namespace THY.Web.Events
{
    public class ApplicationEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RegisterCustomRoutes();
            //Umbraco.Core.Services.ContentService.Publishing += ContentService_Publishing;
        }

        private static void RegisterCustomRoutes()
        {
            RouteTable.Routes.MapRoute(
               name: "BlogSearchController",
               url: "BlogSearch/{action}/{id}",
               defaults: new { controller = "BlogSearch", action = "Index", id = UrlParameter.Optional });

            RouteTable.Routes.MapRoute(
              name: "RSS Feed",
              url: "Feed/",
              defaults: new { controller = "BlogFeeds", action = "rss" });
        }

        //void ContentService_Publishing(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<Umbraco.Core.Models.IContent> e)
        //{
        //    //https://our.umbraco.com/forum/umbraco-7/using-umbraco-7/75719-first-published-date

        //    var contentService = ApplicationContext.Current.Services.ContentService;
        //    foreach (var content in e.PublishedEntities.Where(m => m.HasProperty("publishedDate")))
        //    {
        //        var existingValue = content.GetValue("publishedDate");
        //        if (existingValue == null || existingValue.ToString() == "1/1/0001 12:00:00 AM")
        //        {
        //            var legacyPublishDate = content.GetValue("initialPublishedDate");
        //            if (legacyPublishDate != null)
        //            {
        //                content.SetValue("publishedDate", legacyPublishDate);
        //            }
        //            else
        //            {
        //                content.SetValue("publishedDate", DateTime.Now);
        //            }
                    
        //        }
        //    }

        //}
    }
}