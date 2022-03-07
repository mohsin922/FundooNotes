using CommonLayer.Models;
using RepositoryLayer.entities;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBL
    {
        public bool AddCollaboration(CollabModel collabModel);
        public IEnumerable<Collaborator> GetCollabsbyNoteId(long NotesId);
        public string RemoveCollab(long collabID);
        public IEnumerable<Collaborator> GetAllCollabs();
    }
}
