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
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Answer { get; set; }
        public long? AnswerUserId { get; set; }

        public virtual User AnswerUser { get; set; }
        public virtual Attachment Attachment { get; set; }
        public virtual User User { get; set; }
    }
}
