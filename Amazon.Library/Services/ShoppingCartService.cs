using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public List<ShoppingCart> cartList;

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



        private int NextId
        {
            get
            {
                if (!cartList.Any())
                {
                    return 1;
                }

                return cartList.Select(p => p.Id).Max() + 1;
            }
        }

        public ShoppingCartService()
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

        public ShoppingCart AddNewCart()
        {
            var newCart = new ShoppingCart
            {
                Id = NextId,
                Contents = new List<Product>()
            };
            cartList.Add(newCart);
            return newCart;
        }

        public ShoppingCart? GetCartById(int id)
        {
            return cartList.FirstOrDefault(cart => cart.Id == id);
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

            if (inventoryProduct == null || inventoryProduct.Quantity <= 0)
            {
                // Not enough inventory or product doesn't exist
                return;
            }

            if (existingProduct != null)
            {
                
                existingProduct.Quantity += 1;
            }
            else
            {
                Cart.Contents.Add(new Product
                {
                    Id = newProduct.Id,
                    Name = newProduct.Name,
                    Quantity = 1,
                    Price = newProduct.Price,
                    IsBogo = newProduct.IsBogo,
                    MarkDown = newProduct.MarkDown
                }); ;
                
            }

            inventoryProduct.Quantity -= 1; //makes sense this needed to be last
            UpdateCartPrice();
        }

        public void RemoveFromCart(Product newProduct)
        {
            if (newProduct == null)
            {
                return;
            }


            var existingProduct = Cart.Contents
                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

            if (existingProduct != null)
            {
                if (existingProduct.Quantity > 1)
                {
                    existingProduct.Quantity -= 1;
                }
                else
                {
                    Cart.Contents.Remove(existingProduct);
                }

                var inventoryProduct = InventoryServiceProxy.Current.Products
                    .FirstOrDefault(invProd => invProd.Id == newProduct.Id);

                if (inventoryProduct != null)
                {
                    inventoryProduct.Quantity += 1;
                }

                UpdateCartPrice();
            }
            

        }

        //TAXES
        public decimal actualTaxRate;
        public void UpdateTaxRate(decimal d)
        {
            Cart.TaxRate = d;
            actualTaxRate = d / 100;
            UpdateCartPrice();
        }



        public void UpdateCartPrice()
        {
            Cart.Price = 0;
            foreach(var product in Cart.Contents)
            {
                int amountOfProduct = product.Quantity;
                decimal userPrice = product.Price - product.MarkDown;
                if (product.IsBogo)
                {
                    Cart.Price += (amountOfProduct / 2) * userPrice + (amountOfProduct % 2) * userPrice;
                }
                else
                {
                    Cart.Price += amountOfProduct * userPrice;
                }

            }
            if (actualTaxRate > 0)
            {
                decimal taxAmount = Cart.Price * actualTaxRate;
                Cart.Price += taxAmount;
            }


        }
    }
}