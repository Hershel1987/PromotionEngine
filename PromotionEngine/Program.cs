using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Program
    {
        public static IDictionary<string, decimal> skuCatalog = new Dictionary<string, decimal> {
            {"A", 50m },
            {"B", 30m},
            {"C", 20m},
            {"D", 15m}
            };
        public static IDictionary<string, int> skuIds = new Dictionary<string, int>{
            {"A", 0},
            {"B", 0},
            {"C", 0},
            {"D", 0}
            };

        static void Main(string[] args)
        {
            try
            {
                List<Product> products = new List<Product>();

                Console.WriteLine("total number of order");
                int counter = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < counter; i++)
                {
                    Console.WriteLine("enter the type of product:A,B,C or D");
                    string type = Console.ReadLine();
                    Product p = new Product(type);
                    products.Add(p);
                }

                int totalPrice = GetTotalPrice(products, skuCatalog);
                Console.WriteLine(totalPrice);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generated :" + ex.Message);
            }
        }

        private static int GetTotalPrice(List<Product> products, IDictionary<string, decimal> skuCatalog)
        {
            int totalPriceofA = 0;
            int totalPriceofB = 0;
            int totalPriceofC = 0;
            int totalPriceofD = 0;

            foreach (var item in products)
            {
                if (skuIds.Any(d => d.Key == item.SKUId.ToUpper()))
                {
                    skuIds[item.SKUId.ToUpper()] += 1;
                }                
            }

            totalPriceofA = (skuIds["A"] / 3) * 130 + (skuIds["A"] % 3 * Convert.ToInt32(skuCatalog["A"]));
            totalPriceofB = (skuIds["B"] / 2) * 45 + (skuIds["B"] % 2 * Convert.ToInt32(skuCatalog["B"]));
            totalPriceofC = (skuIds["C"] * Convert.ToInt32(skuCatalog["C"]));
            totalPriceofD = (skuIds["D"] * Convert.ToInt32(skuCatalog["D"]));
            if (skuIds["C"] == 1 && skuIds["D"] == 1)
            {
                totalPriceofC = 30;
                totalPriceofD = 0;
            }
            return totalPriceofA + totalPriceofB + totalPriceofC + totalPriceofD;

        }
    }

    public class Product
    {
        public string SKUId { get; set; }
        public decimal price { get; set; }
        
        public Product(string skuid)
        {
            this.SKUId = skuid;
            if (Program.skuCatalog.Any(d => d.Key == skuid.ToUpper()))
            {
                this.price = Program.skuCatalog[skuid.ToUpper()];
            }         
        }
    }
}
