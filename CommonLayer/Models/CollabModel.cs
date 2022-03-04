using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class CollabModel
    {
        public long NotesId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
    }
}
