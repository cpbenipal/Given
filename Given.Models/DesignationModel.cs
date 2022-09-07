using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class DesignationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsAdd { get; set; }
    }
    public partial class DesignationDDModel
    {
        public Guid Id { get; set; }         
        public string Name { get; set; }
    }
}
