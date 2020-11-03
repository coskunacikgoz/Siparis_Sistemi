using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProject.Models.ViewModels
{
    public class ListProductsViewModel
    {
        public List<Product> Products { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}