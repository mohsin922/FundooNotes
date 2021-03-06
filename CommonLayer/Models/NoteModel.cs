namespace CommonLayer.Models
{
    using System;

    public class NoteModel
    {

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
    }
}