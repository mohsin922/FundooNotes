using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        /// <summary>
        /// Global variables
        /// </summary>
        public readonly FundooContext fundooContext;

        /// <summary>
        /// Constructor
        /// </summary>
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public bool AddCollaboration(CollabModel collabModel)
        {
            try
            {
                var note = this.fundooContext.NotesTable.Where(x => x.NotesId == collabModel.NotesId).SingleOrDefault();
                var user = this.fundooContext.UserTables.Where(x => x.Email == collabModel.EmailId).SingleOrDefault();
                if (note != null && user != null)
                {
                    Collaborator collaborator = new Collaborator
                    {
                        Id = user.Id,
                        NotesId = collabModel.NotesId,
                        CollabEmail = collabModel.EmailId
                    };
                    //Adding the data to database
                    this.fundooContext.CollabTable.Add(collaborator);
                }
                int result = this.fundooContext.SaveChanges();   //Save the changes in database
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Collaborator> GetAllCollabs(long NotesId)
        {
            try
            {
                var result = this.fundooContext.CollabTable.ToList().Where(x => x.NotesId == NotesId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string RemoveCollab(long collabID)
        {
            var collab = fundooContext.CollabTable.Where(X => X.CollabId == collabID).SingleOrDefault();
            if (collab != null)
            {
                fundooContext.CollabTable.Remove(collab);
                this.fundooContext.SaveChanges();
                return "Successfully removed User from collaboration ";
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Method for getting all collaborator
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetAllCollabs()
        {
            try
            {
                var result = this.fundooContext.CollabTable.ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
