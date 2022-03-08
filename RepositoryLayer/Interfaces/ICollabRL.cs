namespace RepositoryLayer.Interfaces
{
    using CommonLayer.Models;
    using RepositoryLayer.entities;
    using System.Collections.Generic;
    public interface ICollabRL
    {
        public bool AddCollaboration(CollabModel collabModel);
        public IEnumerable<Collaborator> GetAllCollabs(long NotesId);
        public string RemoveCollab(long collabID);
        public IEnumerable<Collaborator> GetAllCollabs();
    }
}
