using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class Designation
    {
        public Designation()
        {
            Gift = new HashSet<Gift>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Gift> Gift { get; set; }
    }
}
