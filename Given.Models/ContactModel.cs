using Given.Models.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Given.Models
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public short Prefix { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        [Required]
        [Phone]
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public short Gender { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsAdd { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class ContactDropdownModel
    {
        public Guid Id { get; set; }  
        public string FirstName { get; set; }
    }
}
