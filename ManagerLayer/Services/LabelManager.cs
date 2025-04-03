using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Services
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepo labelRepo;
        public LabelManager(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        //For creating label
        public async Task<LabelEntity> CreateLabelAsync(int UserId, string LabelName)
        {
            return await labelRepo.CreateLabelAsync(UserId, LabelName);
        }

        //To get all labels
        public async Task<List<LabelEntity>> GetLabelsAsync(int userId)
        {
            return await labelRepo.GetLabelsAsync(userId);
        }

        // Assign Label to a Note
        public async Task<NoteLabelEntity> AssignLableAsync(int NoteId, int lableId)
        {
            return await labelRepo.AssignLableAsync(NoteId, lableId);
        }

        public async Task<bool> RemoveLabelFromNoteAsync(int noteId, int labelId)
        {
            return await labelRepo.RemoveLabelFromNoteAsync(noteId, labelId);
        }
    }
}
