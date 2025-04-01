using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollaboratorsRepo : ICollaboratorsRepo
    {
        private readonly FundooDBContext context;

        //dependency
        public CollaboratorsRepo(FundooDBContext context)
        {
            this.context = context;
        }

        //Add collaborator to note
        public CollaboratorsEntity AddCollaborator(int NoteId, int UserId, string Email)
        {
            CollaboratorsEntity entity = new CollaboratorsEntity();
            entity.NoteId = NoteId;
            entity.UserId = UserId;
            entity.Email = Email;

            context.Collaborators.Add(entity);
            context.SaveChanges();
            return entity;
        }

        //Get All Collaborator
        public List<CollaboratorsEntity> GetAllCollaborators(int NoteId, int UserId)
        {
            List<CollaboratorsEntity> collaborators = context.Collaborators.ToList();
            return collaborators;
        }

        //Remove from Collaboration
        public bool RemoveCollaborator(int NoteId, int UserId, int CollaboratorId)
        {
            var collaborator = context.Collaborators.FirstOrDefault(c => c.CollaboratorId == CollaboratorId && c.NoteId == NoteId);
            if (collaborator != null )
            {
                context.Collaborators.Remove(collaborator);
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
