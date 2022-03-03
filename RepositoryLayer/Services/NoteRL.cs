using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
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
        readonly IConfiguration config;

        public NoteRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.config = config;
        }

        /// <summary>
        /// Creating a CreateNote Method
        /// </summary>
        /// <param name="noteModel"></param>
        public bool CreateNote(NoteModel noteModel, long userId)
        {
            try
            {
                Note newNotes = new Note
                {
                    Id = userId,
                    Title = noteModel.Title,
                    NoteBody = noteModel.NoteBody,
                    Reminder = noteModel.Reminder,
                    Color = noteModel.Color,
                    BImage = noteModel.BImage,
                    IsArchived = noteModel.IsArchived,
                    IsPinned = noteModel.IsPinned,
                    IsDeleted = noteModel.IsDeleted,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

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

        public List<Note> RetrieveNote(int NotesId)
        {
            var NoteList = fundooContext.NotesTable.Where(X => X.NotesId == NotesId).SingleOrDefault();
            if (NoteList != null)
            {
                return fundooContext.NotesTable.Where(list => list.NotesId == NotesId).ToList();
            }
            return null;
        }

        public string UpdateNote(NoteModel updateNoteModel, long NotesId)
        {
            try
            {
                var update = fundooContext.NotesTable.Where(X => X.NotesId == NotesId).SingleOrDefault();
                if (update != null && update.NotesId == NotesId)
                {
                    update.Title = updateNoteModel.Title;
                    update.NoteBody = updateNoteModel.NoteBody;
                    update.ModifiedAt = DateTime.Now;
                    update.Color = updateNoteModel.Color;
                    update.BImage = updateNoteModel.BImage;

                    this.fundooContext.SaveChanges();
                    return "Updated";
                }
                else
                {
                    return "Not Updated";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteNotes(long NotesId)
        {
            var deleteNote = fundooContext.NotesTable.Where(X => X.NotesId == NotesId).SingleOrDefault();
            if (deleteNote != null)
            {
                fundooContext.NotesTable.Remove(deleteNote);
                this.fundooContext.SaveChanges();
                return "Notes Deleted Successfully";
            }
            else
            {
                return null;
            }
        }
        public bool IsArchive(long NotesId)
        {
            try
            {
                var model = this.fundooContext.NotesTable.Where(m => m.NotesId == NotesId).SingleOrDefault();

                if (model.IsArchived == false)
                {
                    model.IsArchived = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    model.IsArchived = false;
                    model.ModifiedAt = DateTime.Now;
                    this.fundooContext.NotesTable.Update(model);
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception)
            {
                {
                    throw;
                }
            }
        }
        public bool Pin(long NotesId)
        {
            try
            {
                var PinNote = this.fundooContext.NotesTable.Where(m => m.NotesId == NotesId).SingleOrDefault();

                if (PinNote.IsPinned == false)
                {
                    PinNote.IsPinned = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    PinNote.IsPinned = false;
                    PinNote.ModifiedAt = DateTime.Now;
                    this.fundooContext.NotesTable.Update(PinNote);
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                {
                    throw;
                }
            }
        }
        public bool IsTrash(long NotesId)
        {
            try
            {
                var TrashNote = this.fundooContext.NotesTable.Where(m => m.NotesId == NotesId).SingleOrDefault();

                if (TrashNote.IsDeleted == false)
                {
                    TrashNote.IsDeleted = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    TrashNote.IsDeleted = false;
                    TrashNote.ModifiedAt = DateTime.Now;
                    this.fundooContext.NotesTable.Update(TrashNote);
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                {
                    throw;
                }
            }

        }
        public string UpdateColor(string color, long NotesId)
        {
            try
            {
                var colorNote = this.fundooContext.NotesTable.Where(m => m.NotesId == NotesId).SingleOrDefault();

                if (colorNote != null)
                {
                    colorNote.Color = color;
                    colorNote.ModifiedAt = DateTime.Now;
                    this.fundooContext.NotesTable.Update(colorNote);
                    return this.fundooContext.SaveChanges().ToString();
                }
                else
                {
                    return "Failed to Update";
                }
            }
            catch (Exception)
            {
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// function for adding a background image for a Note
        /// </summary>
        /// <param name="imageURL"></param>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public bool UpdateBgImage(IFormFile imageURL, long NotesId)
        {
            try
            {
                if (NotesId > 0)
                {
                    var note = this.fundooContext.NotesTable.Where(x => x.NotesId == NotesId).SingleOrDefault();
                    if (note != null)
                    {
                        Account acc = new Account(
                            config["Cloudinary:cloud_name"],
                            config["Cloudinary:api_key"],
                            config["Cloudinary:api_secret"]
                            );
                        Cloudinary cloudinary = new Cloudinary(acc);
                        var path = imageURL.OpenReadStream();
                        ImageUploadParams upLoadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(imageURL.FileName, path)
                        };
                        var UploadResult = cloudinary.Upload(upLoadParams);
                        note.BImage = UploadResult.Url.ToString();
                        note.ModifiedAt = DateTime.Now;
                        this.fundooContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
        

  