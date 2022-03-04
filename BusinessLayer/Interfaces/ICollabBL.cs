using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBL
    {
        public bool AddCollaboration(CollabModel collabModel);
    }
}
