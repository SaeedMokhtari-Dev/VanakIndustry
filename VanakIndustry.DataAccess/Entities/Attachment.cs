using System;
using System.Collections.Generic;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class Attachment
    {
        public Attachment()
        {
            PersonCards = new HashSet<Person>();
            PersonPictures = new HashSet<Person>();
            Tickets = new HashSet<Ticket>();
        }

        public long Id { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Person> PersonCards { get; set; }
        public virtual ICollection<Person> PersonPictures { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
