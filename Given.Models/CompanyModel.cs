using System;
using System.Collections.Generic;
using System.Text;

namespace Given.Models
{
    public class UpdateCompanyModel   
    {                                              
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CompanySizeId { get; set; }  
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string CompanyName { get; set; }
        public CompanySizeModel CompanySize { get; set; }
    }
    public class CompanyModel        
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }  
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
        public CompanySizeModel CompanySize { get; set; }  
    }
}
