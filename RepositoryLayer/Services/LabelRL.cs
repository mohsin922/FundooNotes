using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        public readonly FundooContext fundooContext; //context class is used to query or save data to the database.
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public bool CreateLabel(LabelModel labelModel)
        {
            try
            {
                var note = fundooContext.NotesTable.Where(x => x.NotesId == labelModel.NoteId).FirstOrDefault();
                //if (note != null)
                Label label = new Label()
                {
                    LabelName = labelModel.LabelName,
                    NoteId = labelModel.NoteId,
                    UserId = note.Id
                };

                this.fundooContext.LabelTable.Add(label);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
