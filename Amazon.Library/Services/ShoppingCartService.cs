using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Amazon.Library.Services
//{
//    public class ShoppingCartService
//    {
//        private static ShoppingCartService? instance;
//        private static object instanceLock = new object();

//        public ReadOnlyCollection<ShoppingCart> carts;

//        public ShoppingCart Cart
//        {
//            get
//            {
//                if(carts == null || !carts.Any())
//                {
//                    return new ShoppingCart();
//                }
//                return carts?.FirstOrDefault() ?? new ShoppingCart();
//            }
//        }

//        private ShoppingCartService() { }

//        public static ShoppingCartService Current
//        {
//            get
//            {
//                lock (instanceLock)
//                {
//                    if (instance == null)
//                    {
//                        instance = new ShoppingCartService();
//                    }
//                }
//                return instance;
//            }
//        }

//        //public ShoppingCart AddOrUpdate(ShoppingCart c)
//        //{
//        //    //TODO: Someone do this.
//        //}

//        public void AddToCart(Product newProduct)
//        {
//            //if(Cart == null || Cart.Contents == null)
//            //{
//            //    return;
//            //}

//            var existingProduct = Cart?.Contents?
//                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

//            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newProduct.Id);
//            if(inventoryProduct == null)
//            {
//                return;
//            }

//            inventoryProduct.Quantity -= newProduct.Quantity;

//            if(existingProduct != null)
//            {
//                // update
//                existingProduct.Quantity += newProduct.Quantity;
//            } else
//            {
//                //add
//                Cart.Contents.Add(newProduct);
//            }
//        }

//    }
//}
namespace Amazon.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static readonly object instanceLock = new object();

        private List<ShoppingCart> cartList;

        public List<ShoppingCart> Carts => cartList;

        public ShoppingCart Cart
        {
            get
            {
                if (!cartList.Any())
                {
                    var newCart = new ShoppingCart { Contents = new List<Product>() };
                    cartList.Add(newCart);
                    return newCart;
                }
                return cartList.First();
            }
        }

        private ShoppingCartService()
        {
            cartList = new List<ShoppingCart>();
        }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                return instance;
            }
        }

        public void AddToCart(Product newProduct)
        {
            if (newProduct == null)
            {
                return;
            }

            var existingProduct = Cart.Contents
                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

            var inventoryProduct = InventoryServiceProxy.Current.Products
                .FirstOrDefault(invProd => invProd.Id == newProduct.Id);
            if (inventoryProduct == null)
            {
                return;
            }

            inventoryProduct.Quantity -= newProduct.Quantity;

            if (existingProduct != null)
            {
                // update
                existingProduct.Quantity += newProduct.Quantity;
            }
            else
            {
                // add
                Cart.Contents.Add(newProduct);
            }
        }
    }
}