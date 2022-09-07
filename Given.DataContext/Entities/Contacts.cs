using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class Contacts
    {
        public Contacts()
        {
            Gift = new HashSet<Gift>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public short? Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public short? Gender { get; set; }
        public byte[] Photo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public DateTime? Birthday { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Gift> Gift { get; set; }
    }
}
