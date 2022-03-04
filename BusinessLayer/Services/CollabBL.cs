using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public bool AddCollaboration(CollabModel collabModel)
        {
            try
            {
                var result = this.collabRL.AddCollaboration(collabModel);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
