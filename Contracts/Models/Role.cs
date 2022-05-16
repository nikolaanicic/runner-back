using System.Collections.Generic;

namespace Contracts.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string Rolename { get; set; }
        public List<User> UsersInRole { get; set; }
    
    }
}
