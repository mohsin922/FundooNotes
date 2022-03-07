using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collabRL"></param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public bool CreateLabel(LabelModel labelModel)
        {
            try
            {
                return labelRL.CreateLabel(labelModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Label> GetlabelByNotesId(long NotesId)
        {
            try
            {
                return labelRL.GetlabelByNotesId(NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                return labelRL.UpdateLabel(labelModel, labelID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteLabel(long labelID)
        {
            try
            {
                return labelRL.DeleteLabel(labelID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Label> GetAllLabels()
        {
            try
            {
                return labelRL.GetAllLabels();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
