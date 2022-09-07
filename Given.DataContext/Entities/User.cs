using System;
using System.Collections.Generic;

namespace Given.DataContext.Entities
{
    public partial class User
    {
        public User()
        {
            Contacts = new HashSet<Contacts>();
            Designation = new HashSet<Designation>();
            Gift = new HashSet<Gift>();
            UserRole = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid? CompanyId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime? EmailConfirmedOn { get; set; }
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public byte[] Photo { get; set; }
        public Guid? InvitedBy { get; set; }
        public DateTime? InvitedOn { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Contacts> Contacts { get; set; }
        public virtual ICollection<Designation> Designation { get; set; }
        public virtual ICollection<Gift> Gift { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
