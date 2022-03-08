namespace CommonLayer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CollabModel
    {
        public long NotesId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
    }
}
