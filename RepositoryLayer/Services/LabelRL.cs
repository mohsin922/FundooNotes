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

        public IEnumerable<Label> GetAllLabels(long userId)
        {
            try
            {
                var result = this.fundooContext.LabelTable.ToList().Where(x => x.UserId == userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Method to get labels by NotesId
        /// </summary>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        public List<Label> GetlabelByNotesId(long NotesId)
        {
            try
            {
                var response = this.fundooContext.LabelTable.Where(x => x.NoteId == NotesId).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                var update = fundooContext.LabelTable.Where(X => X.LabelID == labelID).FirstOrDefault();
                if (update != null && update.LabelID == labelID)
                {
                    update.LabelName = labelModel.LabelName;
                    update.NoteId = labelModel.NoteId;

                    this.fundooContext.SaveChanges();
                    return "Label is modified";
                }
                else
                {
                    return "Label is not modified";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// DeleteLabel By LabelID
        /// </summary>
        /// <param name="labelID"></param>
        /// <returns></returns>
        public string DeleteLabel(long labelID)
        {
            var deleteLabel = fundooContext.LabelTable.Where(X => X.LabelID == labelID).SingleOrDefault();
            if (deleteLabel != null)
            {
                fundooContext.LabelTable.Remove(deleteLabel);
                this.fundooContext.SaveChanges();
                return "Label is been Deleted Successfully";
            }
            else
            {
                return null;
            }
        }
    }
}
