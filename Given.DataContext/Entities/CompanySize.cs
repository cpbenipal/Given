using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class CompanySize
    {
        public CompanySize()
        {
            Company = new HashSet<Company>();
        }

        public Guid Id { get; set; }
        public string Size { get; set; }
        public short? DisplayOrder { get; set; }

        public virtual ICollection<Company> Company { get; set; }
    }
}
