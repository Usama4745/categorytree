using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class CategoryTreeBuilder
    {
        /// <summary>
        /// Solution 1: Using Entity Framework with LINQ
        /// Builds the tree by querying all categories and recursively building the structure
        /// </summary>
        public static (List<Category> tree, long elapsedMs) BuildTreeWithEFAndLinq()
        {
            var stopwatch = Stopwatch.StartNew();

            using (var context = new CategoryDbContext())
            {
                // Enable query tracking for this solution
                context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;

                // Query all categories with their relationships
                var allCategories = context.Categories
                    .Include(c => c.ChildCategories)
                    .ToList();

                // Build tree structure - get only root categories (ParentCategoryId == null)
                var rootCategories = allCategories
                    .Where(c => c.ParentCategoryId == null)
                    .ToList();

                // Recursively build the tree structure using LINQ
                BuildTreeRecursively(rootCategories, allCategories, 0);

                stopwatch.Stop();
                return (rootCategories, stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Solution 2: Using manual in-memory tree building with optimized queries
        /// This approach uses raw data and builds the tree structure in memory
        /// </summary>
        public static (List<Category> tree, long elapsedMs) BuildTreeWithOptimizedQueries()
        {
            var stopwatch = Stopwatch.StartNew();

            using (var context = new CategoryDbContext())
            {
                // Disable change tracking for better performance with read-only queries
                context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;

                // Query using Select to get only needed data
                var categories = context.Categories
                    .Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        ParentCategoryId = c.ParentCategoryId,
                        ChildCategories = new List<Category>()
                    })
                    .ToList();

                // Build tree structure in memory
                var categoryDict = categories.ToDictionary(c => c.Id);
                var rootCategories = new List<Category>();

                foreach (var category in categories)
                {
                    if (category.ParentCategoryId == null)
                    {
                        rootCategories.Add(category);
                    }
                    else if (categoryDict.ContainsKey(category.ParentCategoryId.Value))
                    {
                        categoryDict[category.ParentCategoryId.Value].ChildCategories.Add(category);
                    }
                }

                // Sort for consistent output
                SortTree(rootCategories);

                stopwatch.Stop();
                return (rootCategories, stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Recursively builds the tree by fetching children for each category
        /// </summary>
        private static void BuildTreeRecursively(List<Category> categories, List<Category> allCategories, int level)
        {
            foreach (var category in categories)
            {
                category.Level = level;
                var children = allCategories
                    .Where(c => c.ParentCategoryId == category.Id)
                    .ToList();

                category.ChildCategories = children;

                if (children.Any())
                {
                    BuildTreeRecursively(children, allCategories, level + 1);
                }
            }
        }

        /// <summary>
        /// Sorts categories by name for consistent display
        /// </summary>
        private static void SortTree(List<Category> categories)
        {
            foreach (var category in categories)
            {
                category.ChildCategories = category.ChildCategories
                    .OrderBy(c => c.Name)
                    .ToList();

                if (category.ChildCategories.Any())
                {
                    SortTree(category.ChildCategories);
                }
            }
        }
    }
}
