namespace BusinessLayer.Interfaces
{
    using CommonLayer.Models;
    using RepositoryLayer.entities;
    using System.Collections.Generic;

    public interface ICollabBL
    {
        public bool AddCollaboration(CollabModel collabModel);
        public IEnumerable<Collaborator> GetCollabsbyNoteId(long NotesId);
        public string RemoveCollab(long collabID);
        public IEnumerable<Collaborator> GetAllCollabs();
    }
}
