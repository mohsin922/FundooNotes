using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IEnumerable<Label> GetAllLabels(long userId)
        {
            try
            {
                return labelRL.GetAllLabels(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Label> GetByLabelID(long labelID)
        {
            try
            {
                return labelRL.GetByLabelID(labelID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
