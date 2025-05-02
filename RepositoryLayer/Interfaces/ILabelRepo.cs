using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepo
    {
        //For creating label
        public Task<LabelEntity> CreateLabelAsync(int UserId, string LabelName);

        //To get all labels
        public Task<List<LabelEntity>> GetLabelsAsync(int userId);

        // To update a label
        Task<LabelEntity> UpdateLabelAsync(int userId, int labelId, string newLabelName);

        // To delete a label
        Task<bool> DeleteLabelAsync(int userId, int labelId);

        // Assign Label to a Note
        public Task<NoteLabelEntity> AssignLableAsync(int NoteId, int lableId);

        //To Remove label from Note
        public Task<bool> RemoveLabelFromNoteAsync(int noteId, int labelId);
    }
}
