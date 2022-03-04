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

        /// <summary>
        /// Function to add a collaborator to a note
        /// </summary>
        /// <param name="collaborator"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddCollaboration(CollabModel collabModel)
        {
            try
            {
                var Collabnote = this.fundooContext.NotesTable.Where(x => x.NotesId == collabModel.NotesId).SingleOrDefault();
                var Collabuser = this.fundooContext.UserTables.Where(x => x.Email == collabModel.EmailId).SingleOrDefault();
                if (Collabnote != null && Collabuser != null)
                {
                    Collaborator collaborator = new Collaborator
                    {
                        Id = Collabuser.Id,
                        NotesId = collabModel.NotesId,
                        CollabEmail = collabModel.EmailId
                    };
                    //Adding the data to database
                    this.fundooContext.CollabTable.Add(collaborator);
                }

                //Save the changes in database
                int result = this.fundooContext.SaveChanges();
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
    }
}
