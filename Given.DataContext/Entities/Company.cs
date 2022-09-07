using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class Company
    {
        public Company()
        {
            User = new HashSet<User>();
        }

        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Guid? CompanySizeId { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual CompanySize CompanySize { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
