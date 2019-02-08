using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THY.Web.Models
{
    public class TagListItem
    {
        public string Text { get; set; }
        public int NodeCount { get; set; }
        public double Weight { get; set; }

        public TagListItem(string text, int nodeCount, double weight)
        {
            Text = text;
            NodeCount = nodeCount;
            Weight = weight;
        }
    }
}