using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace THY.Web.Helpers
{
    public class PresentationHelper
    {
        private readonly UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

        public string FlattenCommaSeparatedList (IEnumerable<string> input, string targetURL = "", string linkClassName = "", bool ordinalize = false, string separator = ", ")
        {
            string output = "";
            int inputSize = input.Count();
            bool isFirstElement = true;
            
            string linkPrefix = "";
            string linkSuffix = "";

            foreach(var s in input)
            {
                if (!string.IsNullOrEmpty(targetURL))
                {
                    linkPrefix = "<a href =\"" + targetURL + "/" + s + "\"";

                    if (!string.IsNullOrEmpty(linkClassName))
                        linkPrefix += " class=\"" + linkClassName + "\"";
                    linkPrefix += ">";
                    linkSuffix = "</a>";
                }

                if (isFirstElement)
                {
                    output += linkPrefix + s + linkSuffix;
                    isFirstElement = false;
                }
                else
                {
                    if (ordinalize)
                    {
                        output += s == input.Last() ? " and " + linkPrefix + s + linkSuffix : separator + linkPrefix + s + linkSuffix;
                    }
                    else
                    {
                        output += separator + linkPrefix + s + linkSuffix;
                    }
                    
                  
                }
            }

            return (output);
        }
        public string GetSvgFile(string fileName)
        {
         string SVGFILEPATH = System.Web.HttpContext.Current.Server.MapPath("~/assets/images/svg/");

        string svgFile = System.IO.File.ReadAllText(SVGFILEPATH + fileName + ".svg");
        return (svgFile);
        }

        public string GetUmbracoMediaUrl(IPublishedContent page, string propertyName)
        {
            int imageId = page.GetPropertyValue<int>(propertyName);
            var mediaItem = umbracoHelper.Media(imageId);

            return (mediaItem.Url);
        }
    }
}