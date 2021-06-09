using System;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class Ticket
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long AttachmentId { get; set; }
        public long PersonId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Answer { get; set; }
        public long? AnswerPersonId { get; set; }

        public virtual Person AnswerPerson { get; set; }
        public virtual Attachment Attachment { get; set; }
        public virtual Person Person { get; set; }
    }
}
