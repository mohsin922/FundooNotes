using System;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer.Models
{
    public class CollabModel
    {
        public long NotesId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
    }
}
