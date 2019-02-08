using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THY.Web.Models
{
    public class BlogPreview
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public int CommentCount { get; set; }

        public BlogPreview(string name, string introduction, string imageUrl, string linkUrl, DateTime publishedDate, string author, int commentCount = 0)
        {
            Name = name;
            Introduction = introduction;
            ImageUrl = imageUrl;
            LinkUrl = linkUrl;
            PublishedDate = publishedDate;
            Author = author;
            CommentCount = commentCount;
        }
    }
}