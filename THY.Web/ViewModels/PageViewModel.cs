using THY.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THY.Web.ViewModels
{
    public class SearchResultsViewModel : PageViewModel
    {
        public BlogSearchResults Results { get; set; }
    }
}