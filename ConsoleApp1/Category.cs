using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Self-referencing foreign key for parent category
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }

        // Collection of child categories
        public virtual List<Category> ChildCategories { get; set; } = new List<Category>();

        // Track level for display purposes
        public int Level { get; set; }
    }
}
