namespace RepositoryLayer.Interfaces
{
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entities;
    using System.Collections.Generic;
    public interface INoteRL
    {
        public bool CreateNote(NoteModel noteModel, long userId);
        public IEnumerable<Note> RetrieveAllNotes(long userId);
        public List<Note> GetAllUserNotes();
        public List<Note> RetrieveNote(int NotesId);
        public string UpdateNote(NoteModel updateNoteModel, long NotesId);
        public string DeleteNotes(long NotesId);

        public bool IsArchive(long NotesId);
        public bool Pin(long Notesid);
        public bool IsTrash(long NotesId);
        public string UpdateColor(string color, long noteid);
        public bool UpdateBgImage(IFormFile imageURL, long NotesId);
    }
}
