using MvcProject.Models.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProject.Models.ViewModels
{
    public class ListCartItemsViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}