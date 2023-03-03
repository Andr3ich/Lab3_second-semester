using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        string pathToJsonFiles = @"C:\JsonFiles\";

        var criteria = new List<Predicate<Product>>();
        criteria.Add(p => p.Price >= 10); 
        criteria.Add(p => p.Category == "Fruit"); 

        for (int i = 1; i <= 10; i++)
        {
            string filePath = Path.Combine(pathToJsonFiles, i.ToString() + ".json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);

                Console.WriteLine($"Products in {i}.json:");
                foreach (var product in products)
                {
                    if (IsMatch(product, criteria))
                    {
                        Console.WriteLine($"Name: {product.Name}, Category: {product.Category}, Price: {product.Price}");
                    }
                }
            }
        }

        Console.ReadLine();
    }
    static bool IsMatch(Product product, List<Predicate<Product>> criteria)
    {
        foreach (var criterion in criteria)
        {
            if (!criterion(product))
            {
                return false;
            }
        }
        return true;
    }
}
class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
}