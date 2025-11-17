using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            using (var context = new CategoryDbContext())
            {
                // Create database if it doesn't exist
                context.Database.EnsureCreated();

                // Check if data already exists
                if (context.Categories.Any())
                {
                    Console.WriteLine("Database already initialized with data.");
                    return;
                }

                Console.WriteLine("Initializing database with sample data...");

                // Create sample category tree with at least 4 levels
                // Level 1: Root Categories
                var electronics = new Category { Name = "Electronics", Description = "Electronic devices and gadgets" };
                var clothing = new Category { Name = "Clothing", Description = "Apparel and fashion items" };
                var food = new Category { Name = "Food & Beverages", Description = "Food and drink products" };

                context.Categories.Add(electronics);
                context.Categories.Add(clothing);
                context.Categories.Add(food);

                context.SaveChanges();

                // Level 2: Subcategories
                var computers = new Category { Name = "Computers", Description = "Desktop and laptop computers", ParentCategoryId = electronics.Id };
                var phones = new Category { Name = "Phones", Description = "Mobile devices", ParentCategoryId = electronics.Id };
                var accessories = new Category { Name = "Accessories", Description = "Electronic accessories", ParentCategoryId = electronics.Id };

                var menClothing = new Category { Name = "Men's Clothing", Description = "Clothing for men", ParentCategoryId = clothing.Id };
                var womenClothing = new Category { Name = "Women's Clothing", Description = "Clothing for women", ParentCategoryId = clothing.Id };

                var beverages = new Category { Name = "Beverages", Description = "Drinks", ParentCategoryId = food.Id };
                var snacks = new Category { Name = "Snacks", Description = "Snack foods", ParentCategoryId = food.Id };

                context.Categories.AddRange(computers, phones, accessories, menClothing, womenClothing, beverages, snacks);
                context.SaveChanges();

                // Level 3: Sub-subcategories
                var laptops = new Category { Name = "Laptops", Description = "Portable computers", ParentCategoryId = computers.Id };
                var desktops = new Category { Name = "Desktops", Description = "Desktop computers", ParentCategoryId = computers.Id };
                var monitors = new Category { Name = "Monitors", Description = "Computer monitors", ParentCategoryId = accessories.Id };

                var smartphones = new Category { Name = "Smartphones", Description = "Smart mobile phones", ParentCategoryId = phones.Id };
                var tablets = new Category { Name = "Tablets", Description = "Tablet devices", ParentCategoryId = phones.Id };

                var shirts = new Category { Name = "Shirts", Description = "Men's shirts", ParentCategoryId = menClothing.Id };
                var pants = new Category { Name = "Pants", Description = "Men's pants", ParentCategoryId = menClothing.Id };

                var dresses = new Category { Name = "Dresses", Description = "Women's dresses", ParentCategoryId = womenClothing.Id };
                var skirts = new Category { Name = "Skirts", Description = "Women's skirts", ParentCategoryId = womenClothing.Id };

                var coffee = new Category { Name = "Coffee", Description = "Coffee beverages", ParentCategoryId = beverages.Id };
                var tea = new Category { Name = "Tea", Description = "Tea beverages", ParentCategoryId = beverages.Id };

                var chips = new Category { Name = "Chips & Crisps", Description = "Crispy snacks", ParentCategoryId = snacks.Id };
                var candy = new Category { Name = "Candy", Description = "Sweet snacks", ParentCategoryId = snacks.Id };

                context.Categories.AddRange(laptops, desktops, monitors, smartphones, tablets,
                    shirts, pants, dresses, skirts, coffee, tea, chips, candy);
                context.SaveChanges();

                // Level 4: Deep subcategories
                var gamingLaptops = new Category { Name = "Gaming Laptops", Description = "High-performance gaming laptops", ParentCategoryId = laptops.Id };
                var businessLaptops = new Category { Name = "Business Laptops", Description = "Professional business laptops", ParentCategoryId = laptops.Id };

                var dellDesktops = new Category { Name = "Dell Desktops", Description = "Dell brand desktops", ParentCategoryId = desktops.Id };
                var hpDesktops = new Category { Name = "HP Desktops", Description = "HP brand desktops", ParentCategoryId = desktops.Id };

                var samsungPhones = new Category { Name = "Samsung Phones", Description = "Samsung smartphones", ParentCategoryId = smartphones.Id };
                var iphonePhones = new Category { Name = "iPhone", Description = "Apple iPhones", ParentCategoryId = smartphones.Id };

                var formalShirts = new Category { Name = "Formal Shirts", Description = "Formal dress shirts", ParentCategoryId = shirts.Id };
                var casualShirts = new Category { Name = "Casual Shirts", Description = "Casual shirts", ParentCategoryId = shirts.Id };

                var espresso = new Category { Name = "Espresso", Description = "Espresso coffee", ParentCategoryId = coffee.Id };
                var americano = new Category { Name = "Americano", Description = "Americano coffee", ParentCategoryId = coffee.Id };

                var softCandy = new Category { Name = "Soft Candy", Description = "Soft candy items", ParentCategoryId = candy.Id };
                var hardCandy = new Category { Name = "Hard Candy", Description = "Hard candy items", ParentCategoryId = candy.Id };

                context.Categories.AddRange(gamingLaptops, businessLaptops, dellDesktops, hpDesktops,
                    samsungPhones, iphonePhones, formalShirts, casualShirts, espresso, americano,
                    softCandy, hardCandy);
                context.SaveChanges();

                Console.WriteLine("Database initialized successfully with sample hierarchical data!");
                Console.WriteLine($"Total categories created: {context.Categories.Count()}");
            }
        }
    }
}
