﻿using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public bool CreateNote(NoteModel noteModel);
        public IEnumerable<Note> GetAllNotes();
    }
}
