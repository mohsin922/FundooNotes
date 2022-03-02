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


        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;

        }

        /// <summary>
        /// Creating a CreateNote Method
        /// </summary>
        /// <param name="noteModel"></param>
        public bool CreateNote(NoteModel noteModel,long userId)
        {
            try
            {
                Note newNotes = new Note();
                newNotes.Id = userId;
                newNotes.NotesId = noteModel.NotesId;
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
        /// Show all Notes to user
        /// </summary>
        public IEnumerable<Note> RetrieveAllNotes(long userId)
        {
            try
            {
                var result = this.fundooContext.NotesTable.ToList().Where(x => x.Id == userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}