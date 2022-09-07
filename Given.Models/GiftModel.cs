using Given.Models;
using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public class GiftModel 
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
        public bool IsAdd { get; set; }
    }
    public class GiftListModel       
    {
        public Guid Id { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }  
        public DateTime? GiftDate { get; set; }
        public decimal? Amount { get; set; }
        public string Designation { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? UserId { get; set; }     
        public DateTime CreatedOn { get; set; }   
    }
}
