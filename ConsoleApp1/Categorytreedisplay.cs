using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CategoryTreeDisplay
    {
        /// <summary>
        /// Displays the category tree in the console with indentation
        /// </summary>
        public static void DisplayTree(List<Category> categories, string title)
        {
            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('=', 80));

            if (categories == null || categories.Count == 0)
            {
                Console.WriteLine("No categories to display.");
                return;
            }

            foreach (var category in categories)
            {
                DisplayCategoryRecursive(category, 0);
            }
        }

        /// <summary>
        /// Recursively displays category and its children with tree formatting
        /// </summary>
        private static void DisplayCategoryRecursive(Category category, int level)
        {
            string indent = GetIndent(level);
            string prefix = level == 0 ? "• " : "├─ ";

            Console.WriteLine($"{indent}{prefix}{category.Name}");
            Console.WriteLine($"{indent}   └─ {category.Description}");

            // Display child categories
            if (category.ChildCategories != null && category.ChildCategories.Count > 0)
            {
                for (int i = 0; i < category.ChildCategories.Count; i++)
                {
                    var child = category.ChildCategories[i];
                    DisplayCategoryRecursive(child, level + 1);
                }
            }
        }

        /// <summary>
        /// Creates indentation string based on tree level
        /// </summary>
        private static string GetIndent(int level)
        {
            return new string(' ', level * 3);
        }

        /// <summary>
        /// Displays performance comparison results
        /// </summary>
        public static void DisplayPerformanceComparison(
            (List<Category> tree, long elapsedMs) efSolution,
            (List<Category> tree, long elapsedMs) optimizedSolution)
        {
            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("  PERFORMANCE COMPARISON RESULTS");
            Console.WriteLine(new string('=', 80));

            Console.WriteLine("\n1. Entity Framework + LINQ Solution:");
            Console.WriteLine($"   Execution Time: {efSolution.elapsedMs} ms");
            Console.WriteLine($"   Categories Count: {CountCategories(efSolution.tree)}");

            Console.WriteLine("\n2. Optimized Query Solution:");
            Console.WriteLine($"   Execution Time: {optimizedSolution.elapsedMs} ms");
            Console.WriteLine($"   Categories Count: {CountCategories(optimizedSolution.tree)}");

            long difference = efSolution.elapsedMs - optimizedSolution.elapsedMs;
            double percentageDifference = efSolution.elapsedMs > 0
                ? (difference / (double)efSolution.elapsedMs) * 100
                : 0;

            Console.WriteLine("\n" + new string('-', 80));
            if (difference > 0)
            {
                Console.WriteLine($"   Optimized solution is FASTER by {Math.Abs(difference)} ms ({Math.Abs(percentageDifference):F2}%)");
            }
            else if (difference < 0)
            {
                Console.WriteLine($"   EF + LINQ solution is FASTER by {Math.Abs(difference)} ms ({Math.Abs(percentageDifference):F2}%)");
            }
            else
            {
                Console.WriteLine("   Both solutions have equal performance");
            }
            Console.WriteLine(new string('=', 80));
        }

        /// <summary>
        /// Counts total categories in the tree
        /// </summary>
        private static int CountCategories(List<Category> categories)
        {
            int count = 0;

            foreach (var category in categories)
            {
                count += 1 + CountCategoriesRecursive(category.ChildCategories);
            }

            return count;
        }

        private static int CountCategoriesRecursive(List<Category> categories)
        {
            int count = 0;

            if (categories != null)
            {
                foreach (var category in categories)
                {
                    count += 1 + CountCategoriesRecursive(category.ChildCategories);
                }
            }

            return count;
        }

        /// <summary>
        /// Displays tree depth analysis
        /// </summary>
        public static void DisplayTreeAnalysis(List<Category> categories)
        {
            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("  TREE ANALYSIS");
            Console.WriteLine(new string('=', 80));

            int depth = GetTreeDepth(categories);
            int totalCount = CountCategories(categories);

            Console.WriteLine($"   Maximum Tree Depth: {depth} levels");
            Console.WriteLine($"   Total Categories: {totalCount}");
            Console.WriteLine(new string('=', 80));
        }

        /// <summary>
        /// Gets the maximum depth of the tree
        /// </summary>
        private static int GetTreeDepth(List<Category> categories)
        {
            int maxDepth = 1;

            foreach (var category in categories)
            {
                int childDepth = GetChildDepth(category.ChildCategories);
                maxDepth = Math.Max(maxDepth, childDepth + 1);
            }

            return maxDepth;
        }

        private static int GetChildDepth(List<Category> categories)
        {
            if (categories == null || categories.Count == 0)
                return 0;

            int maxDepth = 0;

            foreach (var category in categories)
            {
                int childDepth = GetChildDepth(category.ChildCategories);
                maxDepth = Math.Max(maxDepth, childDepth + 1);
            }

            return maxDepth;
        }
    }
}
