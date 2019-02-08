using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THY.Web.Models
{
    public class HtmlMeta
    {
        public string SiteName { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string PageCanonical { get; set; }

        public string OpenGraphTitle { get; set; }
        public string OpenGraphDescription { get; set; }
        public string OpenGraphImage { get; set; }
        public string OpenGraphSiteName { get; set; }

        public string TwitterCardSite { get; set; }
        public string TwitterCardImageAlt { get; set; }

        public HtmlMeta(string siteName, string pageTitle, string pageDescription, string pageCanonical, string openGraphSiteName, string twitterCardSite,  string openGraphTitle = "", string openGraphDescription = "", string openGraphImage = "", string twitterCardImageAlt = "")
        {
            SiteName = siteName;
            PageTitle = pageTitle;
            PageDescription = pageDescription;
            PageCanonical = pageCanonical;
            OpenGraphSiteName = openGraphSiteName;
            TwitterCardSite = twitterCardSite;
            OpenGraphTitle = openGraphTitle == "" ? pageTitle : openGraphTitle;
            OpenGraphDescription = openGraphDescription == "" ? pageDescription : openGraphDescription;
            OpenGraphImage = openGraphImage;
            TwitterCardImageAlt = twitterCardImageAlt;


        }
    }
}
