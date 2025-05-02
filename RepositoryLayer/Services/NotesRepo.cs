using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;
        public NotesRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //Adding new Note
        public NotesEntity CreateNote(int userId, NotesModel notesModel)
        {
            NotesEntity notes = new NotesEntity();

            notes.UserId = userId;

            notes.Title = notesModel.Title;
            notes.Description = notesModel.Description;
            notes.Colour = notesModel.Colour;

            //adding values in DB
            context.Add(notes);
            context.SaveChanges();
            return notes;
        }

        //To get all notes
        public List<NotesEntity> GetAllNotes(int UserId)
        {
            //var note = context.Notes.FirstOrDefault(n => n.UserId == UserId);

            return context.Notes
                                .Where(n => n.UserId == UserId)
                                .Include(n => n.NoteLabels)                // Include join table
                                .ThenInclude(nl => nl.Label)               // Include actual label data
                                .ToList();
        }

        //Update Note
        public NotesEntity UpdateNote(int noteId, NotesModel model, int userId)
        {
            var note = context.Notes.FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            if (note != null)
            {
                note.Title = model.Title;
                note.Description = model.Description;
                note.Remainder = note.Remainder;
                context.SaveChanges();
                return note;
            }
            else
            {
                return null;
            }
        }

        //Delete Note
        public bool DeleteNote (int noteId, int userId)
        {
            var note = context.Notes.FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            if ( note != null)
            {
                context.Notes.Remove(note);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //Fetch Notes using title
        public List<NotesEntity> GetNotesByTitleOrDescription(string searchText)
        {
            return context.Notes
                .Where(note => note.Title.Contains(searchText) || note.Description.Contains(searchText))
                .ToList();
        }

        //Return Count of notes a user has
        public int CountAllNotes()
        {
            return context.Notes.Count();
        }

        //To Pin note
        public bool PinNote(int noteId, int userId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            if (note != null)
            {
                if (note.IsPinned)
                {
                    note.IsPinned = false;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsPinned = true;
                    context.SaveChanges();
                    return true;
                }
          
            }
            else
            {
                return false;
            }
        }

        //To archive Note
        public bool ArchiveNote(int noteId, int userId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            if (note != null)
            {
                if (note.IsArchived && note.IsPinned == false)
                {
                    note.IsArchived = false;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsArchived = true;
                    note.IsPinned = false;
                    context.SaveChanges();
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        //To archive Note
        public bool TrashNote(int noteId, int userId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            if (note != null)
            {
                if (note.IsTrashed)
                {
                    note.IsTrashed = false;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsTrashed = true;
                    context.SaveChanges();
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        //To Add Colour of Note
        public bool AddColourToNote(int noteId, string colour, int userId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            if (note != null)
            {
                note.Colour = colour;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }



        //To Add Remainder to Note
        public bool AddRemainderToNote(int noteId, DateTime remainder, int userId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            if (note != null)
            {
                note.Remainder = remainder;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //To adding Image to Note
        public bool AddImage( int NoteId, int UserId , IFormFile image)
        {
            NotesEntity notesEntity = context.Notes.ToList().Find(n => n.NotesId == NoteId && n.UserId == UserId);
            if (notesEntity != null)
            {
                Account account = new Account(
                    configuration["CloudinarySettings:CloudName"],
                    configuration["CloudinarySettings:ApiKey"],
                    configuration["CloudinarySettings:ApiSecret"]                
                    );
                Cloudinary cloudinary = new Cloudinary(account);

                var UploadParameters = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())

                };

                var UploadResult = cloudinary.Upload(UploadParameters);
                string ImagePath = UploadResult.Url.ToString();
                notesEntity.Image = ImagePath;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
       }

    }
}
