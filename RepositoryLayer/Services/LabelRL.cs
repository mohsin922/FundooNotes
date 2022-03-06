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
        public List<Label> GetByLabelID(long labelID)
        {
            var label = fundooContext.LabelTable.Where(X => X.LabelID == labelID).SingleOrDefault();
            if (label != null)
            {
                return fundooContext.LabelTable.Where(X => X.LabelID == labelID).ToList();
            }
            return null;
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
