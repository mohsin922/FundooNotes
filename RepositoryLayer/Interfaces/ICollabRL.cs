using CommonLayer.Models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICollabRL
    {
        public bool AddCollaboration(CollabModel collabModel);
        public IEnumerable<Collaborator> GetAllCollabs(long NotesId);
        public string RemoveCollab(long collabID);
        public IEnumerable<Collaborator> GetAllCollabs();
    }
}
