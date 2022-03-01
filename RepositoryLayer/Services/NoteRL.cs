using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        public readonly FundooContext fundooContext;
        IConfiguration _configure;

        public NoteRL(FundooContext fundooContext, IConfiguration configure)
        {
            this.fundooContext = fundooContext;
            _configure = configure;
        }

        /// <summary>
        /// Creating a CreateNote Method
        /// </summary>
        /// <param name="noteModel"></param>
        public bool CreateNote(NoteModel noteModel)
        {
            try
            {
                Note newNotes = new Note();
                newNotes.NotedId = noteModel.NotedId;
                newNotes.Title = noteModel.Title;
                newNotes.NoteBody = noteModel.NoteBody;
                newNotes.Reminder = noteModel.Reminder;
                newNotes.Color = noteModel.Color;
                newNotes.BImage = noteModel.BImage;
                newNotes.IsArchived = noteModel.IsArchived;
                newNotes.IsPinned = noteModel.IsPinned;
                newNotes.IsDeleted = noteModel.IsDeleted;
                newNotes.CreatedAt = DateTime.Now;
                newNotes.ModifiedAt = DateTime.Now;
                
                this.fundooContext.NotesTable.Add(newNotes); //Adding  data to database
                //Save the changes in database
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Show all his notes to user
        /// </summary>
        public IEnumerable<Note> GetAllNotes()
        {
            try
            {
                return this.fundooContext.NotesTable.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}