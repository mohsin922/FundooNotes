using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        public bool CreateLabel(LabelModel labelModel);
        public List<Label> GetlabelByNotesId(long NotesId);
        public string UpdateLabel(LabelModel labelModel, long labelID);
        public string DeleteLabel(long labelID);
        public IEnumerable<Label> GetAllLabels();
    }
}
