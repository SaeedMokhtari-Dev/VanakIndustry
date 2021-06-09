using System;
using System.Collections.Generic;

#nullable disable

namespace VanakIndustry.DataAccess.Entities
{
    public partial class Person
    {
        public Person()
        {
            ElectionCandidates = new HashSet<ElectionCandidate>();
            ElectionResults = new HashSet<ElectionResult>();
            RefreshTokens = new HashSet<RefreshToken>();
            SelectElectionCandidates = new HashSet<SelectElectionCandidate>();
            TicketAnswerPeople = new HashSet<Ticket>();
            TicketPeople = new HashSet<Ticket>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string Barcode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string BirthDate { get; set; }
        public long? CardId { get; set; }
        public long? PictureId { get; set; }
        public bool IsActive { get; set; }
        public long RoleId { get; set; }
        public bool Present { get; set; }
        public string MotherName { get; set; }
        public string CertificateId { get; set; }
        public string NickName { get; set; }
        public long FirstPageCertificateId { get; set; }
        public long NationalCardId { get; set; }
        public bool Married { get; set; }
        public long? SecondPageCertificateId { get; set; }
        public string Qualification { get; set; }
        public string SkillDescription { get; set; }
        public string FieldOfStudy { get; set; }
        public string PostalCode { get; set; }
        public string MoreDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public virtual Attachment Card { get; set; }
        public virtual Attachment Picture { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<ElectionCandidate> ElectionCandidates { get; set; }
        public virtual ICollection<ElectionResult> ElectionResults { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<SelectElectionCandidate> SelectElectionCandidates { get; set; }
        public virtual ICollection<Ticket> TicketAnswerPeople { get; set; }
        public virtual ICollection<Ticket> TicketPeople { get; set; }
    }
}
