﻿using CommonLayer.Model;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public List<NotesEntity> GetAllNotes(int UserId)
        {
            return notesRepo.GetAllNotes(UserId);
        }

        //Update Note
        public NotesEntity UpdateNote(int noteId, NotesModel model, int userId)
        {
            return notesRepo.UpdateNote(noteId, model, userId);
        }

        //Delete Note
        public bool DeleteNote(int noteId, int userId)
        {
            return notesRepo.DeleteNote(noteId, userId);
        }

        //Fetch Notes using title
        public List<NotesEntity> GetNotesByTitleOrDescription(string searchText)
        {
            return notesRepo.GetNotesByTitleOrDescription(searchText);
        }

        //Return Count of notes a user has
        public int CountAllNotes()
        {
            return notesRepo.CountAllNotes();
        }

        //To Pin note
        public bool PinNote(int noteId, int userId)
        {
            return notesRepo.PinNote(noteId, userId);
        }

        //Archive
        public bool ArchiveNote(int noteId, int userId)
        {
            return notesRepo.ArchiveNote(noteId, userId);
        }

        //Trash
        public bool TrashNote(int noteId, int userId)
        {
            return notesRepo.TrashNote(noteId, userId);
        }

        //To Add Colour of Note
        public bool AddColourToNote(int noteId, string colour, int userId)
        {
            return notesRepo.AddColourToNote(noteId, colour, userId);
        }

        //To Add Remainder to Note
        public bool AddRemainderToNote(int noteId, DateTime remainder, int userId)
        {
            return notesRepo.AddRemainderToNote(noteId, remainder, userId);
        }

        //To adding Image to Note
        public bool AddImage(int NoteId, int UserId, IFormFile image)
        {
            return notesRepo.AddImage(NoteId, UserId, image);
        }
    }
}
