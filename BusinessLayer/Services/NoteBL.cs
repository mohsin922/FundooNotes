namespace BusinessLayer.Services
{
    using BusinessLayer.Interfaces;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entities;
    using RepositoryLayer.Interfaces;
    using System;
    using System.Collections.Generic;

    public class NoteBL : INoteBL
    {
        private readonly INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        /// <summary>
        /// Adding a new Note Method
        /// </summary>
        /// <param name="noteModel"></param>
        public bool CreateNote(NoteModel noteModel, long userId)
        {
            try
            {
                return this.noteRL.CreateNote(noteModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Note> RetrieveAllNotes(long userId)
        {
            try
            {
                return this.noteRL.RetrieveAllNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Note> GetAllUserNotes()
        {
            try
            {
                return noteRL.GetAllUserNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Note> RetrieveNote(int NotesId)
        {
            try
            {
                return this.noteRL.RetrieveNote(NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateNote(NoteModel updateNoteModel, long NotesId)
        {
            try
            {
                return noteRL.UpdateNote(updateNoteModel, NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteNotes(long NotesId)
        {
            try
            {
                return noteRL.DeleteNotes(NotesId);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Un/Archive a note
        /// </summary>
        public bool IsArchive(long NotesId)
        {
            try
            {
                var result = this.noteRL.IsArchive(NotesId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Pin/UnPin a note
        /// </summary>
        public bool Pin(long NotesId)
        {
            try
            {
                var result = this.noteRL.Pin(NotesId);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Trash a note
        /// </summary>
        public bool IsTrash(long NotesId)
        {
            try
            {
                var result = this.noteRL.IsTrash(NotesId);
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Note Color
        public string UpdateColor(string color, long NotesId)
        {
            try
            {
                var result = this.noteRL.UpdateColor(color, NotesId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateBgImage(IFormFile imageURL, long NotesId)
        {
            try
            {
                var result = this.noteRL.UpdateBgImage(imageURL, NotesId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
