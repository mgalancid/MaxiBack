using System;
using System.Collections.Generic;

namespace Back.Models
{
    public partial class Users
    {
        public int IdUsers { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string State { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int IdRole { get; set; }
        public virtual Roles IdRoleNavigation { get; set; }
    }
}
