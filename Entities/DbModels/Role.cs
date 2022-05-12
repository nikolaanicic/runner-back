using System.Collections.Generic;

namespace Entities.DbModels
{
    public class Role
    {
        public long Id { get; set; }
        public string Rolename { get; set; }
        public List<User> UsersInRole { get; set; }
    
    }
}
