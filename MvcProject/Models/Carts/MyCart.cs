using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProject.Models.Carts
{
    public class MyCart
    {
        Dictionary<int, CartItem> _myCarts = new Dictionary<int, CartItem>();

        public decimal TotalPrice { get; internal set; }
        public List<CartItem> CartItems { get; internal set; }

        //public List<CartItem> CartItems => _myCarts.Values.ToList();

        // public decimal TotalPrice => _myCarts.Sum(x => x.Value.SubPrice);

        public void Add(CartItem cartItem)
        {
            if (_myCarts.ContainsKey(cartItem.ID))
            {
                _myCarts[cartItem.ID].Amount += cartItem.Amount;
                return;
            }

            _myCarts.Add(cartItem.ID, cartItem);
        }

        public void Delete(int id)
        {
            if (_myCarts[id].Amount > 1)
            {
                _myCarts[id].Amount -= 1;
                return;
            }

            _myCarts.Remove(id);
        }

        public void Update(params short[] amounts)
        {
            for (int i = 0; i < amounts.Length; i++)
            {
                _myCarts.ElementAt(i).Value.Amount = amounts[i];
            }
        }
    }
}