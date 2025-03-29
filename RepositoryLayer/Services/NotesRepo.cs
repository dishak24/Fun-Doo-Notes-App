using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooDBContext context;

        public NotesRepo(FundooDBContext context)
        {
            this.context = context;
        }

        //Adding new Note
        public NotesEntity CreateNote(int userId, NotesModel notesModel)
        {
            NotesEntity notes = new NotesEntity();

            notes.UserId = userId;

            notes.Title = notesModel.Title;
            notes.Description = notesModel.Description;

            //adding values in DB
            context.Add(notes);
            context.SaveChanges();
            return notes;
        }

        //To get all notes
        public List<NotesEntity> GetAllNotes()
        {
            List<NotesEntity> allNotes = context.Notes.ToList();
            return allNotes;
        }

        //Fetch Notes using title
        public List<NotesEntity> GetNotesByTitle(string title)
        {
            List<NotesEntity> notes = context.Notes.Where( n => n.Title == title).ToList();
            return notes;
        }

        //Return Count of notes a user has
        public int CountAllNotes()
        {
            return context.Notes.Count();
        }
    }
}
