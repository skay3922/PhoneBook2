using System;
using System.Collections.Generic;

namespace PhoneBook.Data
{
    public partial class PhoneBook : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Organization { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string UserId { get;set; }
    }
}
