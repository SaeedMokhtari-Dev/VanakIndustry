using System.Collections.Generic;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class Role
    {
        public Role()
        {
            People = new HashSet<Person>();
        }

        public long Id { get; set; }
        public string Role1 { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
