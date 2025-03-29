using CommonLayer.Model;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepo notesRepo;

        public NotesManager(INotesRepo notesRepo)
        {
            this.notesRepo = notesRepo;
        }

        //Adding new Note
        public NotesEntity CreateNote(int userId, NotesModel notesModel)
        {
            return notesRepo.CreateNote(userId, notesModel);
        }

        //Get all notes
        public List<NotesEntity> GetAllNotes()
        {
            return notesRepo.GetAllNotes();
        }

        //Fetch Notes using title
        public List<NotesEntity> GetNotesByTitle(string title)
        {
            return notesRepo.GetNotesByTitle(title);
        }

        //Return Count of notes a user has
        public int CountAllNotes()
        {
            return notesRepo.CountAllNotes();
        }
    }
}
