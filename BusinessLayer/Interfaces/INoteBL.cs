﻿using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        public bool CreateNote(NoteModel noteModel, long userId);
        public IEnumerable<Note> RetrieveAllNotes(long userId);
        public List<Note> RetrieveNote(int NotesId);
        public string UpdateNote(NoteModel updateNoteModel, long NoteId);
    }
}
