using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface INotesManager
    {
        //Create Note
        public NotesEntity CreateNote(int userId, NotesModel notesModel);

        //Get all notes
        public List<NotesEntity> GetAllNotes();

        //Update Note
        public NotesEntity UpdateNote(int noteId, NotesModel model, int userId);

        //Fetch Notes using title
        public List<NotesEntity> GetNotesByTitle(string title);

        //Return Count of notes a user has
        public int CountAllNotes();
    }
}
