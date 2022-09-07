using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class Category
    {
        public Category()
        {
            Designation = new HashSet<Designation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Designation> Designation { get; set; }
    }
}
