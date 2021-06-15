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
            UserCandidatePictures = new HashSet<User>();
            UserCards = new HashSet<User>();
            UserFirstPageCertificates = new HashSet<User>();
            UserNationalCards = new HashSet<User>();
            UserPictures = new HashSet<User>();
            UserSecondPageCertificates = new HashSet<User>();
        }

        public long Id { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<User> UserCandidatePictures { get; set; }
        public virtual ICollection<User> UserCards { get; set; }
        public virtual ICollection<User> UserFirstPageCertificates { get; set; }
        public virtual ICollection<User> UserNationalCards { get; set; }
        public virtual ICollection<User> UserPictures { get; set; }
        public virtual ICollection<User> UserSecondPageCertificates { get; set; }
    }
}
