using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THY.Web.Models
{
    public class ArchiveYear
    {
        public int Year { get; set; }

        public List<ArchiveMonth> Months { get; set; }

        public int ArticleCount { get; set; }

        public ArchiveYear(int year, List<ArchiveMonth> month)
        {
            Year = year;
            Months = month;
        
        }
    }
}