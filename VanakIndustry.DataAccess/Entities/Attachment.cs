using System;
using System.Collections.Generic;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class Attachment
    {
        public Attachment()
        {
            Tickets = new HashSet<Ticket>();
            UserCards = new HashSet<User>();
            UserPictures = new HashSet<User>();
        }

        public long Id { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<User> UserCards { get; set; }
        public virtual ICollection<User> UserPictures { get; set; }
    }
}
