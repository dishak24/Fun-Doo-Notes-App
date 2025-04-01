using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRepo
    {
        //Create note
        public NotesEntity CreateNote(int userId, NotesModel notesModel);

        //Get all notes
        public List<NotesEntity> GetAllNotes();

        //Update Note
        public NotesEntity UpdateNote(int noteId, NotesModel model, int userId);

        //Delete Note
        public bool DeleteNote(int noteId, int userId);

        //Fetch Notes using title
        public List<NotesEntity> GetNotesByTitle(string title);

        //Return Count of notes a user has
        public int CountAllNotes();

        //To Pin note
        public bool PinNote(int noteId, int userId);

        //To Archive note
        public bool ArchiveNote(int noteId, int userId);

        //To archive Note
        public bool TrashNote(int noteId, int userId);

        //To Add Colour of Note
        public bool AddColourToNote(int noteId, string colour, int userId);

        //To Add Remainder to Note
        public bool AddRemainderToNote(int noteId, DateTime remainder, int userId);

        //To adding Image to Note
        public bool AddImage(int NoteId, int UserId, IFormFile image);

    }
}
