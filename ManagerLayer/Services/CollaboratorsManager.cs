using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class CollaboratorsManager : ICollaboratorsManager
    {
        //Dependency
        private readonly ICollaboratorsRepo collaboratorsRepo;

        public CollaboratorsManager(ICollaboratorsRepo collaboratorsRepo)
        {
            this.collaboratorsRepo = collaboratorsRepo;
        }
        //Add collaborator to note
        public CollaboratorsEntity AddCollaborator(int NoteId, int UserId, string Email)
        {
            return collaboratorsRepo.AddCollaborator(NoteId, UserId, Email);
        }

        //Get All Collaborator
        public List<CollaboratorsEntity> GetAllCollaborators(int NoteId, int UserId)
        {
            return collaboratorsRepo.GetAllCollaborators(NoteId, UserId);
        }

        //Remove from Collaboration
        public bool RemoveCollaborator(int NoteId, int UserId, int CollaboratorId)
        {
            return collaboratorsRepo.RemoveCollaborator(NoteId, UserId, CollaboratorId);
        }
    }
}
