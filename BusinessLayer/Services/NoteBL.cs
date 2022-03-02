using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
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
                return this.noteRL.CreateNote(noteModel,userId);
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
    }
}
