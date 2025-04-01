using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface ICollaboratorsManager
    {
        //Add collaborator to note
        public CollaboratorsEntity AddCollaborator(int NoteId, int UserId, string Email);

        //Get All Collaborator
        public List<CollaboratorsEntity> GetAllCollaborators(int NoteId, int UserId);

        //Remove from Collaboration
        public bool RemoveCollaborator(int NoteId, int UserId, int CollaboratorId);
    }
}
