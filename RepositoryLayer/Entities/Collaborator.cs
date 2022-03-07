using RepositoryLayer.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.entities
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }

        [ForeignKey("note")]
        public long NotesId { get; set; }
        public virtual Note note { get; set; }

        [ForeignKey("user")]
        public long Id { get; set; }
        public virtual User user { get; set; }
    }
}
