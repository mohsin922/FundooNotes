using CommonLayer.Models;
using RepositoryLayer.Entities;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public bool CreateLabel(LabelModel labelModel);
        public List<Label> GetlabelByNotesId(long NotesId);
        public string UpdateLabel(LabelModel labelModel, long labelID);
        public string DeleteLabel(long labelID);
        public IEnumerable<Label> GetAllLabels();
    }
}