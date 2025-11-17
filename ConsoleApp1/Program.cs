using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          CATEGORY TREE PERFORMANCE COMPARISON - ENTITY FRAMEWORK          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝\n");

            try
            {
                // Step 1: Initialize the database with sample data
                Console.WriteLine("STEP 1: Initializing Database...");
                DatabaseInitializer.InitializeDatabase();

                Console.WriteLine("\n" + new string('-', 80));

                // Step 2: Build tree using EF + LINQ (Solution 1)
                Console.WriteLine("\nSTEP 2: Building Category Tree using Entity Framework + LINQ...");
                var efSolution = CategoryTreeBuilder.BuildTreeWithEFAndLinq();
                Console.WriteLine($"✓ Completed in {efSolution.elapsedMs} ms");

                // Step 3: Build tree using optimized queries (Solution 2)
                Console.WriteLine("\nSTEP 3: Building Category Tree using Optimized Queries...");
                var optimizedSolution = CategoryTreeBuilder.BuildTreeWithOptimizedQueries();
                Console.WriteLine($"✓ Completed in {optimizedSolution.elapsedMs} ms");

                Console.WriteLine("\n" + new string('-', 80));

                // Step 4: Display the category trees
                Console.WriteLine("\nSTEP 4: Displaying Category Trees...");

                // Display first tree
                CategoryTreeDisplay.DisplayTree(efSolution.tree,
                    "CATEGORY TREE - Solution 1: EF + LINQ");

                // Display second tree
                CategoryTreeDisplay.DisplayTree(optimizedSolution.tree,
                    "CATEGORY TREE - Solution 2: Optimized Queries");

                // Step 5: Display performance comparison
                CategoryTreeDisplay.DisplayPerformanceComparison(efSolution, optimizedSolution);

                // Step 6: Display tree analysis
                CategoryTreeDisplay.DisplayTreeAnalysis(efSolution.tree);

                // Step 7: Additional Analysis
                DisplayDetailedAnalysis(efSolution.tree);

                Console.WriteLine("\n✓ Application completed successfully!");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error occurred: {ex.Message}");
                Console.WriteLine($"Details: {ex.InnerException?.Message}");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Displays detailed analysis of the category tree
        /// </summary>
        private static void DisplayDetailedAnalysis(System.Collections.Generic.List<Category> categories)
        {
            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("  DETAILED CATEGORY ANALYSIS");
            Console.WriteLine(new string('=', 80));

            AnalyzeCategoriesByLevel(categories, 0);

            Console.WriteLine(new string('=', 80));
        }

        /// <summary>
        /// Recursively analyzes categories at each level
        /// </summary>
        private static void AnalyzeCategoriesByLevel(System.Collections.Generic.List<Category> categories, int level)
        {
            if (categories == null || categories.Count == 0)
                return;

            string levelName = GetLevelName(level);
            Console.WriteLine($"\n   {levelName}:");
            Console.WriteLine($"   Count: {categories.Count} items");

            foreach (var category in categories.Take(3))
            {
                Console.WriteLine($"      • {category.Name}");
            }

            if (categories.Count > 3)
            {
                Console.WriteLine($"      ... and {categories.Count - 3} more");
            }

            // Analyze children
            var allChildren = categories
                .Where(c => c.ChildCategories != null && c.ChildCategories.Count > 0)
                .SelectMany(c => c.ChildCategories)
                .ToList();

            if (allChildren.Count > 0)
            {
                AnalyzeCategoriesByLevel(allChildren, level + 1);
            }
        }

        /// <summary>
        /// Gets a friendly name for each tree level
        /// </summary>
        private static string GetLevelName(int level)
        {
            return level switch
            {
                0 => "Level 1 - Root Categories",
                1 => "Level 2 - Main Subcategories",
                2 => "Level 3 - Detailed Categories",
                3 => "Level 4 - Specific Items",
                _ => $"Level {level + 1}"
            };
        }
    }
}
