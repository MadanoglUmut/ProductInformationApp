using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsProductApp.Models
{
    public class Repository
    {
        private static readonly List<Product> products = new();

        private static readonly List<Categorty> categorties = new();

        static Repository(){
            categorties.Add(new Categorty{CategoryId = 1 , CategortyName = "Telefon"});
            categorties.Add(new Categorty{CategoryId = 2, CategortyName = "Bilgisayar"});
            
            products.Add(new Product{ProductId = 1, Name = "Iphone 14", Price =15000,Image="1.jpg",CategoryId=1,IsActive = true});
            products.Add(new Product{ProductId = 2, Name = "Iphone 15", Price =25000,Image="2.jpg",CategoryId=1,IsActive = false});
            products.Add(new Product{ProductId = 3, Name = "Macbook", Price =45000,Image="5.jpg",CategoryId=2,IsActive = true});
            products.Add(new Product{ProductId = 4, Name = "Macbook-Pro", Price =65000,Image="6.jpg",CategoryId=2,IsActive = false});
        }

        public static List<Product> Products {
            get{
                return products;
               }
        }

        public static void AddProdutct(Product product){
            products.Add(product);
        }

        public static void UpdateProduct(Product uproduct){
            var updateProduct = products.FirstOrDefault(p=>p.ProductId == uproduct.ProductId);
            if(uproduct != null){
                updateProduct.Name = uproduct.Name;
                updateProduct.Price = uproduct.Price;
                updateProduct.CategoryId = uproduct.CategoryId;
                updateProduct.Image = uproduct.Image;
                updateProduct.IsActive = uproduct.IsActive;
            }
        }

        public static void Delete(Product deleteProduct){
            var entitiy = products.FirstOrDefault(p=>p.ProductId == deleteProduct.ProductId);
            if(deleteProduct != null){
                products.Remove(entitiy);
            }

        }

        public static void UpdateIsActive(Product product){
            var entitiy = products.FirstOrDefault(p=>p.ProductId == product.ProductId);
            if(entitiy != null){
                entitiy.IsActive = product.IsActive;
            }
        }


        public static List<Categorty> Categorties {
            get{
                return categorties;
            }
        }
    }
}