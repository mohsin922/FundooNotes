namespace RepositoryLayer.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long NotesId { get; set; }
        public string Title { get; set; }
        public string NoteBody { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
        public string Color { get; set; }
        public string BImage { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        //foreign keys
        [ForeignKey("user")]
        public long Id { get; set; }
        public virtual User user { get; set; }
    }
}
