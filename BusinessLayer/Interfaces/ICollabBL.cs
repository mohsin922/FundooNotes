using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.entities;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
