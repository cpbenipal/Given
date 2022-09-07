using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class Gift
    {
        public Guid Id { get; set; }
        public Guid? ContactId { get; set; }
        public string Description { get; set; }
        public DateTime? GiftDate { get; set; }
        public decimal? Amount { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual Contacts Contact { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual User User { get; set; }
    }
}
