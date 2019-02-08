using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THY.Web.Models
{
    public class ArchiveMonth
    {
        public int Month { get; set; }

        public string DisplayText { get; set; }

        public int ArticleCount { get; set; }

        public Dictionary<string, string> ArticleLinks { get; set; }

        public ArchiveMonth(int month, string displayText, Dictionary<string,string> articleLinks)
        {
            Month = month;
            DisplayText = displayText;
            ArticleLinks = articleLinks;
        }
    }
}